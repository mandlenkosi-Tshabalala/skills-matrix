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

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;
                    editContext = new EditContext(personEducation);

                    personEducations = await EducationService.GetEducations(user.Id);
                    if (personEducation != null)
                    {
                        editContext = new EditContext(personEducation);
                    }
                }
            }

        }

        protected  async void HandleValidSubmit()
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
            NavigationManager.NavigateTo($"/address/{UserId}");
        }

        protected void Next()
        {
            NavigationManager.NavigateTo("/personEmpolyment");
        }
    }
}
