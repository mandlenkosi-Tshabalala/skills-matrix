using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using SkillsMatrix.Models;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SelectPdf;

namespace SkillsMatrix.Web.Pages.Employees
{
    public class EmployeeListBase : ComponentBase
    {

        [Inject]
        public IToastService toastService { get; set; }

        [Inject]
        public IPersonService PersonService { get; set; }

        [Inject]
        public IActivityService ActivityService { get; set; }

        [Inject]
        public ISkillsService SkillsService { get; set; }

        [Inject]
        public IPersonService personService { get; set; }

        [Inject]
        public IPersonCompetencies PersonCompetencies { get; set; }

        [Inject]
        public IPersonExpertiseService PersonExpertiseService { get; set; }

        [Inject]
        public IAddressService AddressService { get; set; }
        [Inject]
        public IEducationService educationService { get; set; }

        [Inject]
        public IEmployementHistoryService employementHistoryService { get; set; }

        [Inject]
        public IExpertiseService ExpertiseService { get; set; }

        [Inject]
        public ICompetenciesCategoryService CompetenciesCategoryService { get; set; }

        [Inject]
        public ICompetenciesService CompetenciesService { get; set; }

        protected PersonalInfo Person = new PersonalInfo();

        protected Address address = new Address();
        protected List<Skill> skills = new List<Skill>();
        protected List<Education> educations = new List<Education>();
        protected List<Employment> employments = new List<Employment>();
        protected List<UserActivities> activities = new List<UserActivities>();
        protected List<UserCompetency> userCompetencies = new List<UserCompetency>();
        protected List<UserExpertise> expertises = new List<UserExpertise>();

        public IEnumerable<PersonalInfo> Employees { set; get; }

        [Inject]
        Microsoft.JSInterop.IJSRuntime JS { get; set; }

        public string EmployeeName { set; get; }

        public string Skills { set; get; }

        public string QualificationLevel { set; get; }

        public string Country { set; get; }

        public bool SelectAll { set; get; }

        public string expertiseID { get; set; }

        public string competencyCategoryID { get; set; }

        public string competencyID { get; set; }

        public bool Searching = true;

        public int Percentage;

        protected bool IsDownloading { get; set; }

        public IEnumerable<Expertise> functionalList { get; set; }

        public IEnumerable<CompetencyCategory> competencyCategoryList { get; set; }

        protected List<Competency> competencies { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = await PersonService.GetAllEmployees("", 0, 0, "", "", "", 0);
            functionalList = await ExpertiseService.GetAll();
            competencyCategoryList = await CompetenciesCategoryService.GetCompetencies();

            //foreach(var emp in Employees)
            //{
            //    emp.CvProgress = await ProfileCompletion(emp.UserId);
            //}


            Searching = false;

        }

        protected async Task Search()
        {
            Searching = true;
            this.StateHasChanged();

            Employees = await PersonService.GetAllEmployees(EmployeeName, Convert.ToInt32(expertiseID), Convert.ToInt32(competencyCategoryID), Skills, QualificationLevel, Country, Convert.ToInt32(competencyID));
            //foreach (var emp in Employees)
            //{
            //    emp.CvProgress = await ProfileCompletion(emp.UserId);
            //}
            if (Employees.Count() == 0)
            {
                Employees = null;
            }

            EmployeeCheckedList.Clear();

            Searching = false;
            this.StateHasChanged();
        }

        public async Task DownloadCV()
        {
            IsDownloading = true;
            this.StateHasChanged();

            if (EmployeeCheckedList.Count == 0)
            {
                toastService.ShowError("Please ensure at least one employee is selected for CV Download.", "Error");
            }

            else
            {
                try
                {
                    await ExportToPDF();
                    EmployeeCheckedList.Clear();
                }

                catch (Exception ex)
                {
                    toastService.ShowError("Error downloading CV", "Error");
                }
            }

            IsDownloading = false;
            this.StateHasChanged();
        }

        protected void ViewCV(int ViewUserID)
        {

            NavigationManager.NavigateTo($"/viewCV/" + ViewUserID.ToString());
        }

        public List<PersonalInfo> EmployeeCheckedList { get; set; } = new List<PersonalInfo>();
        protected void empSelect(PersonalInfo emp, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (EmployeeCheckedList.Count == Employees.Count())
                {
                    SelectAll = true;
                }
                
                if (!EmployeeCheckedList.Any(x => x.Id == emp.Id))
                {
                    EmployeeCheckedList.Add(emp);
                }
            }
            else
            {
                SelectAll = false;

                if (EmployeeCheckedList.Any(x => x.Id == emp.Id))
                {
                    EmployeeCheckedList.Remove(emp);
                }
            }
        }

        protected void empSelectAll(object checkedValue)
        {

            if ((bool)checkedValue)
            {
                foreach (var item in Employees)
                {
                    if (!EmployeeCheckedList.Any(x => x.Id == item.Id))
                    {
                        EmployeeCheckedList.Add(item);
                    }
                }

                SelectAll = true;
            }
            else
            {
                foreach (var item in Employees)
                {
                    if (EmployeeCheckedList.Any(x => x.Id == item.Id))
                    {
                        EmployeeCheckedList.Remove(item);
                    }
                }

                SelectAll = false;
            }

            this.StateHasChanged();
        }

        protected bool empCheckSelectStatus(int empID)
        {
            if (EmployeeCheckedList.Any(x => x.Id == empID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void functionalClicked(object functional)
        {
            expertiseID = functional.ToString();
        }

        protected async void competencyCategoryClicked(object competencyCategory)
        {
            competencyCategoryID = competencyCategory.ToString();
            competencies = await CompetenciesService.GetAll(Convert.ToInt32(competencyCategoryID));
            this.StateHasChanged();
        }

        public static byte[] GetZipArchive(List<InMemoryFile> files)
        {
            byte[] archiveFile;
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open())
                            zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }


                archiveFile = archiveStream.ToArray();
            }

            return archiveFile;
        }

        protected async Task<string> ExportToPDF()
        {
            return await Task.Run(async () =>
            {
                string pdf_page_size = "A4";
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), pdf_page_size, true);

            int webPageWidth = 1024;
            try
            {
                webPageWidth = Convert.ToInt32("1024");
            }
            catch { }

            int webPageHeight = 0;
            try
            {
                webPageHeight = Convert.ToInt32("0");
            }
            catch { }

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;
            converter.Options.MarginBottom = 15;
            converter.Options.MarginTop = 15;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            // create a new pdf document converting an url

            List<InMemoryFile> files = new List<InMemoryFile>();
            byte[] zip;

            foreach (var item in EmployeeCheckedList)
            {
                string url = "";

                url = NavigationManager.ToAbsoluteUri("/viewPDF/" + item.UserId.ToString()).ToString();

                PdfDocument doc = converter.ConvertUrl(url);
                MemoryStream pdfStream = new MemoryStream();
                
                doc.Save(pdfStream);
                doc.Save();
                doc.Close();
                
                files.Add(new InMemoryFile() { FileName = item.FirstName + "_" + item.LastName + "_" + string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now) + "_CV.pdf", Content = pdfStream.ToArray() });
                
                pdfStream.Close();
                pdfStream.Dispose();
            }

            if (files.Count == 1)
            {
                await JS.SaveAs(files[0].FileName, files[0].Content);

            }

            else
            {
                zip = GetZipArchive(files);
                await JS.SaveAs($"DownloadCV_" + string.Format("{0:yyyy-MM-dd_HH-mm-ss-fff}", DateTime.Now) + ".zip", zip.ToArray());
            }

                return "Task Complete";
            });
        }

        public async Task<int> ProfileCompletion(int UserId)
        {
            int Total = 0;
            int t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0;

            Person = await PersonService.GetPersonByUserId(UserId);
            address = await AddressService.Get(UserId);
            skills = await SkillsService.GetSkills(UserId);
            educations = await educationService.GetEducations(UserId);
            employments = await employementHistoryService.GetEmployment(UserId);
            activities = await ActivityService.GetActivity(UserId);
            expertises = await PersonExpertiseService.GetAll(UserId);
            userCompetencies = await PersonCompetencies.GetAll(UserId);

            if (Person != null)
            {
                t1 = 20;
            }
            if (address != null)
            {
                t2 = 20;
            }
            if (educations.Count != 0)
            {
                t3 = 10;
            }
            if (employments.Count != 0)
            {
                t4 = 10;
            }
            if (activities.Count != 0)
            {
                t5 = 10;
            }
            if (expertises.Count != 0)
            {
                t6 = 10;
            }
            if (userCompetencies.Count != 0)
            {
                t7 = 10;
            }
            if (skills.Count != 0)
            {
                t8 = 10;
            }

            Total = t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8;

            return Total;
        }
    }

    public class InMemoryFile
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
