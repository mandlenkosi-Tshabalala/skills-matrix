using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Models;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Shared
{
    public class TopNavBase : ComponentBase
    {

        [Inject]
        public IPersonService PersonService { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }

        protected PersonalInfo Person = new PersonalInfo();

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;



            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;
                    Person = await PersonService.GetPersonByUserId(UserId);
                }
            }

        }

    }
}
