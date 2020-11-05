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
using IronPdf;
//using System.Web.Hosting;

namespace SkillsMatrix.Web.Pages.CVFlow.NewFolderForm
{
    public class CVBasecs : ComponentBase
    {
        [Inject]
        public IActivityService ActivityService { get; set; }

        [Inject]
        public ISkillsService SkillsService { get; set; }
        [Inject]
        public IPersonService personService { get; set; }

        [Inject]
        public IAddressService AddressService { get; set; }

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

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;


            if (ViewUserId > 0)
            {
                personalInfo = await personService.GetPersonByUserId(ViewUserId);
                address = await AddressService.Get(ViewUserId);
                skills = await SkillsService.GetSkills(ViewUserId);
                educations = await educationService.GetEducations(ViewUserId);
                employments = await employementHistoryService.GetEmployment(ViewUserId);
                activities = await ActivityService.GetActivity(ViewUserId);
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

                    }
                }
            }

        }


        protected void Back()
        {
            NavigationManager.NavigateTo($"/membership");
        }

        public void Download()
        {
            try
            {
                // Create a PDF from a web page
                var Renderer = new HtmlToPdf();
                var PDF = Renderer.RenderUrlAsPdf("https://en.wikipedia.org/wiki/Portable_Document_Format");
                PDF.SaveAs("mpho.pdf");

                // This neat trick opens our PDF file so we can see the result
                //System.Diagnostics.Process.Start("wikipedia.pdf");
            }
            catch(Exception ex)
            {

            }
        }



    }
}