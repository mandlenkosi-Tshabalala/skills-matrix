using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Models;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Pages.Dashboard
{
    public class DashBoardBase : ComponentBase
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        [Inject]
        public IPersonService personService { get; set; }

        [Inject]
        public ISkillsService SkillsService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }


        public int UserId { get; set; }

        protected PersonalInfo personalInfo = new PersonalInfo();
        protected Address address = new Address();
        protected List<Skill> skills = new List<Skill>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            ClaimsPrincipal user = (await AuthState).User;

            if (user.Identity.IsAuthenticated)
            {
                Console.WriteLine($"{user.Identity.Name} is authenticated.");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;


            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;

                    personalInfo = await personService.GetPersonByUserId(UserId);
                    skills = await SkillsService.GetSkills(UserId);

                }
            }

        }

        public void ViewCV()
        {
            NavigationManager.NavigateTo($"/viewCV");
        }
    }
}
