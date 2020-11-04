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
//using System.Web.Hosting;

namespace SkillsMatrix.Web.Pages.CVFlow.NewFolderForm
{
    public class CVBasecs : ComponentBase
    {
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


                    }
                }
            }

        }


        protected void Back()
        {
            NavigationManager.NavigateTo($"/membership");
        }



    }
}