
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Web.Services;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;

namespace SkillsMatrix.Web.Pages.AdminTool
{
    public class CompetencyFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IExpertiseService expertiseService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private bool edit = false;

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }
        protected Expertise expertise = new Expertise();
        protected List<Expertise> expertises = new List<Expertise>();
        protected EditContext editContext;

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(expertise);

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;


                    expertises = await expertiseService.GetAll();


                }
            }

        }

        protected async void HandleValidSubmit()
        {


            if (edit == false)
            {

                await expertiseService.Create(expertise);
                await OnInitializedAsync();
                NavigationManager.NavigateTo($"/adminExpertise");
                expertise = new Expertise();

            }
            else
            {
                await expertiseService.Update(expertise);
                await OnInitializedAsync();
                edit = false;
                NavigationManager.NavigateTo($"/adminExpertise");
                expertise = new Expertise();


            }

        }


        protected async Task GetExpertise(int id)
        {
            expertise = await expertiseService.Get(id);
            edit = true;

        }

        protected async Task DeleteExpertise(int id)
        {
            await expertiseService.Delete(id);

            await OnInitializedAsync();
            NavigationManager.NavigateTo($"/adminExpertise");
            expertise = new Expertise();


        }
    }
}