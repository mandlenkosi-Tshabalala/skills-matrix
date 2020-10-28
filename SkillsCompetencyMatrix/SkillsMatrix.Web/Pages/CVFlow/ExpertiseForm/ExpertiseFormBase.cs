using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Blazored.Toast.Services;

namespace SkillsMatrix.Web.Pages.CVFlow.ExpertiseForm
{
    public class ExpertiseFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IPersonExpertiseService UserExpertiseService { get; set; }

        [Inject]
        public IExpertiseService ExpertiseService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }

        protected List<UserExpertise> userExpertises = new List<UserExpertise>();

        protected IEnumerable<Expertise> Expertises; 

        protected UserExpertise userExpertise = new UserExpertise();

        protected EditContext editContext;

        protected string expertiseID { get; set; }

        private bool edit = false;

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(userExpertise);

            if (principalUser.Identity.IsAuthenticated)
            {

              

                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;
                    Expertises = await ExpertiseService.GetAll();
                 
                    userExpertises = await UserExpertiseService.GetAll(user.Id);


                }
            }

        }

        protected async void HandleValidSubmit()
        {


            if (edit == false)
            {
                userExpertise.UserId = UserId;
                userExpertise.ExpertiseId = Int32.Parse(expertiseID);
                await UserExpertiseService.Create(userExpertise);
                await OnInitializedAsync();
                NavigationManager.NavigateTo($"/expertise");
                userExpertise = new UserExpertise();

            }
            else
            {
                await UserExpertiseService.Update(userExpertise);
                await OnInitializedAsync();
                edit = false;
                NavigationManager.NavigateTo($"/expertise");
                userExpertise = new UserExpertise();


            }

        }

        protected async void HandleDelete()
        {

            if (userExpertise.Id == 0)
            {

                await UserExpertiseService.Create(userExpertise);
                NavigationManager.NavigateTo($"/personEmpolyment");
            }
            else
            {
                await UserExpertiseService.Update(userExpertise);
                NavigationManager.NavigateTo($"/personEmpolyment");
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/personEmpolyment");
        }

        protected void Next()
        {
            NavigationManager.NavigateTo("/personCompetencies");
        }

        protected async void Cancel()
        {
            userExpertise = new UserExpertise();
            NavigationManager.NavigateTo($"/expertise");

        }


        protected async Task GetExpertise(int id)
        {
            userExpertise = await UserExpertiseService.Get(id);
            edit = true;

        }

        protected async Task DeleteExpertise(int id)
        {

            if (id != 0)
            {
                await UserExpertiseService.Delete(id);

                await OnInitializedAsync();
                NavigationManager.NavigateTo($"/expertise");
                userExpertise = new UserExpertise();
            }


        }
    }
}
