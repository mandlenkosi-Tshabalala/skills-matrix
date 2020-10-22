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

namespace SkillsMatrix.Web.Pages.CVFlow.EducationForm
{
    public class EducationFormBase : ComponentBase
    {

        [Inject]
        public IEducationService EducationService { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }

        protected List<Education> personEducations = new List<Education>();

        protected Education personEducation = new Education();

        protected EditContext editContext;

        private bool edit = false;

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(personEducation);

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;

                 
                    personEducations = await EducationService.GetEducations(user.Id);


                }
            }

        }

        protected  async void HandleValidSubmit()
        {


            if (edit == false)
             {
                personEducation.UserId = UserId;
                await EducationService.Create(personEducation);
                await OnInitializedAsync();
                NavigationManager.NavigateTo($"/personEducation");
                personEducation = new Education();

            }
            else
            {
                await EducationService.Update(personEducation);
                await OnInitializedAsync();
                edit = false;
                NavigationManager.NavigateTo($"/personEducation");
                personEducation = new Education();


            }

        }

        protected async void HandleDelete()
        {

            if (personEducation.Id == 0)
            {

                await EducationService.Create(personEducation);
                NavigationManager.NavigateTo($"/personEmpolyment");
            }
            else
            {
                await EducationService.Update(personEducation);
                NavigationManager.NavigateTo($"/personEmpolyment");
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/address");
        }

        protected void Next()
        {
            NavigationManager.NavigateTo("/personEmpolyment");
        }

        protected async void Cancel()
        {
            personEducation = new Education();
            NavigationManager.NavigateTo($"/personEducation");

        }


        protected async Task GetEducation(int id)
        {
            personEducation = await EducationService.Get(id);
            edit = true;

        }

        protected async Task DeleteEducation(int id)
        {
            await EducationService.Delete(id);

            await OnInitializedAsync();
            NavigationManager.NavigateTo($"/personEducation");
            personEducation = new Education();


        }
    }
}
