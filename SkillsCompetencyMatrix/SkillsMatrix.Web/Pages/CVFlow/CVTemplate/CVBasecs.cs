using Blazored.Toast.Services;
using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SkillsMatrix.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IO;
using SelectPdf;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using SkillsMatrix.Web.Services.Shared;





//using System.Web.Hosting;

namespace SkillsMatrix.Web.Pages.CVFlow.NewFolderForm
{
    public class CVBasecs : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }

        [Inject]
        public IActivityService ActivityService { get; set; }


        [Inject]
        public ISkillsService SkillsService { get; set; }
        [Inject]
        public IPersonService personService { get; set; }

        [Inject]
        public IAddressService AddressService { get; set; }

        [Inject]
        Microsoft.JSInterop.IJSRuntime JS { get; set; }

        [Inject]
        public IEducationService educationService { get; set; }

        [Inject]
        public IEmployementHistoryService employementHistoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }

        private bool edit = false;

        public bool Searching = true;

        protected bool IsDownloading { get; set; }

        [Parameter]
        public string PersonId { get; set; }

        [Parameter]
        public int ViewUserId { get; set; }

        protected PersonalInfo personalInfo = new PersonalInfo();
        protected Address address = new Address();
        protected List<Skill> skills = new List<Skill>();
        protected List<Education> educations = new List<Education>();
        protected List<Employment> employments = new List<Employment>();
        protected List<UserActivities> activities = new List<UserActivities>();
        public string EducationMessage  = "Not Completed";
        public string WorkHistoryMessage = "Present";

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;
            Searching = true;


            if (ViewUserId > 0)
            {
                personalInfo = await personService.GetPersonByUserId(ViewUserId);
                address = await AddressService.Get(ViewUserId);
                skills = await SkillsService.GetSkills(ViewUserId);
                educations = await educationService.GetEducations(ViewUserId);
                employments = await employementHistoryService.GetEmployment(ViewUserId);
                activities = await ActivityService.GetActivity(ViewUserId);
                Searching = false;


            }
            else
            {
                if (principalUser.Identity.IsAuthenticated)
                {
                    var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                    if (user != null)
                    {
                        UserId = user.Id;

                        personalInfo = await personService.GetPersonByUserId(UserId);
                        address = await AddressService.Get(UserId);
                        skills = await SkillsService.GetSkills(UserId);
                        educations = await educationService.GetEducations(UserId);
                        employments = await employementHistoryService.GetEmployment(UserId);
                        activities = await ActivityService.GetActivity(UserId);
                        Searching = false;

                        if (personalInfo.CvProgress != 100)
                        {
                            toastService.ShowWarning("Your cv is not 100% completed , please go to Cv flow and complete.", "Warning");
                        }

                    }
                }

            }

        }


        protected void Back()
        {
            NavigationManager.NavigateTo($"/membership");
        }

        public async Task Download()
        {

            IsDownloading = true;
            this.StateHasChanged();

            try
            {
                await ExportToPDF();
            }

            catch (Exception ex)
            {
                toastService.ShowError("Error downloading CV", "Error");
            }

            IsDownloading = false;
            this.StateHasChanged();
        }

        protected async Task<string> ExportToPDF()
        {
   
            return await Task.Run(async () =>
            {
                var principalUser = (await AuthState).User;

                if (principalUser.Identity.IsAuthenticated)
                {
                    var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                    if (user != null)
                    {
                        UserId = user.Id;
                    }
                }
                string url = "";

                if (ViewUserId == 0)
                {
                    //url = $"https://localhost:44349/viewPDF/{UserId}";
                    url = NavigationManager.ToAbsoluteUri("/viewPDF/" + UserId.ToString()).ToString();
                    personalInfo = await personService.GetPersonByUserId(UserId);
                }
                else
                {
                    //url = $"https://localhost:44349/viewPDF/{ViewUserId}";
                    url = NavigationManager.ToAbsoluteUri("/viewPDF/" + ViewUserId.ToString()).ToString();
                    personalInfo = await personService.GetPersonByUserId(ViewUserId);
                }

                string pdf_page_size = "A4";
                PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                    pdf_page_size, true);

                string pdf_orientation = "Portrait";
                //PdfPageOrientation pdfOrientation =
                //    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                //    pdf_orientation, true);

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
                //  converter.Options.PdfPageOrientation = pdfOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                converter.Options.MarginBottom = 15;
                converter.Options.MarginTop = 15;
                converter.Options.MarginLeft = 10;
                converter.Options.MarginRight = 10;
                // create a new pdf document converting an url
                PdfDocument doc = converter.ConvertUrl(url);

                MemoryStream ms = new MemoryStream();
                doc.Save(ms);
                doc.Save();


                //Download the PDF in the browser.
                await JS.SaveAs($"{personalInfo.FirstName} {personalInfo.LastName} CV.pdf", ms.ToArray());

                return "Task Complete";
            });
        }


    }
}