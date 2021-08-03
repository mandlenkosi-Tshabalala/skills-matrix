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
using Microsoft.AspNetCore.Identity;

namespace SkillsMatrix.Web.Pages.AdminTool.RolesForm
{
    public class UserRoleFormBase : ComponentBase
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

        public List<PersonalInfo> EmployeeCheckedList { get; set; } = new List<PersonalInfo>();
        [Parameter]
        public int RoleId { get; set; }

        public Role Role { get; set; }
        [Inject]
        public IRolesService RolesService { get; set; }

        public IList<IdentityUser<int>> UsersInRole { get; set; }
        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Role = await RolesService.Get(RoleId);
            UsersInRole = await UserManager.GetUsersInRoleAsync(Role.Name);
            foreach (var userInRole in UsersInRole)
            {
                var checkedUser = await personService.GetPersonByUserId(userInRole.Id);
                EmployeeCheckedList.Add(checkedUser);
            }
            Employees = await PersonService.GetAllEmployees("", 0, 0, "", "", "", 0);
            functionalList = await ExpertiseService.GetAll();
            competencyCategoryList = await CompetenciesCategoryService.GetCompetencies();


            Searching = false;

        }

        protected async Task Search()
        {
            Searching = true;
            this.StateHasChanged();

            Employees = await PersonService.GetAllEmployees(EmployeeName, Convert.ToInt32(expertiseID), Convert.ToInt32(competencyCategoryID), Skills, QualificationLevel, Country, Convert.ToInt32(competencyID));

            if (Employees.Count() == 0)
            {
                Employees = null;
            }

            EmployeeCheckedList.Clear();

            Searching = false;
            this.StateHasChanged();
        }

        public void Clear()
        {
            EmployeeName = "";
            Skills = "";
            QualificationLevel = "";
            Country = "";
            expertiseID = "0";
            competencyCategoryID = "0";
            competencyID = "0";
            functionalList = null;
            competencyCategoryList = null;

        }
        public void BackToRole()
        {
            NavigationManager.NavigateTo("/adminRole");
        }
        public async Task AddUserToRole()
        {
            var usersInRoleList = new List<int>();
            foreach (var user in UsersInRole)
            {
                usersInRoleList.Add(user.Id);
            }


            foreach (var employee in EmployeeCheckedList)
            {
                //if (!usersInRoleList.Contains(employee.Id))
                //{
                    var newUser = await UserManager.FindByIdAsync(employee.UserId.ToString());
                    if (!await UserManager.IsInRoleAsync(newUser,Role.Name))
                    {
                        try
                        {
                            await UserManager.AddToRoleAsync(newUser, Role.Name);

                        }
                        catch (Exception ex)
                        {
                            toastService.ShowError("There was an error when trying to save", "Error");
                        }
                    }
                    
                    

                //}
            }


            var employeesInCheckedList = new List<int>();
            foreach(var employee in EmployeeCheckedList)
            {
                employeesInCheckedList.Add(employee.UserId);
            }

            foreach (var removedUser in UsersInRole)
            {
                if (!employeesInCheckedList.Contains(removedUser.Id))
                {
                    try
                    {
                        await UserManager.RemoveFromRoleAsync(removedUser, Role.Name);
                    }
                    catch (Exception ex)
                    {
                        toastService.ShowError("There was an error when trying to save", "Error");
                    }

                }
            }

            toastService.ShowSuccess("Role successfully edited", "Saved");
            IsDownloading = true;
            this.StateHasChanged();

        }


        
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
                    //foreach (var employee in EmployeeCheckedList)
                    //{
                    //    if (employee.Id== emp.Id)
                    //    {
                    //        EmployeeCheckedList.Remove(employee);
                    //    }
                    //}


                    for (int i=0;i< EmployeeCheckedList.Count;i++)
                    {
                        if (EmployeeCheckedList[i].Id == emp.Id)
                        {
                            EmployeeCheckedList.Remove(EmployeeCheckedList[i]);
                            //i = 0;
                        }
                    }

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

    }

    public class InMemoryFile
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
