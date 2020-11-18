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
using Blazored.Toast.Services;

namespace SkillsMatrix.Web.Pages.CVFlow.EducationForm
{
    public class EducationFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
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

            editContext = new EditContext(personEducation);

            var principalUser = (await AuthState).User;



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
            var isValid = editContext.Validate();

            if (isValid)
            {

                if (edit == false)
                {
                    try
                    {
                        personEducation.UserId = UserId;
                        await EducationService.Create(personEducation);
                        personEducation = new Education();
                        await OnInitializedAsync();
                        this.StateHasChanged();
                        toastService.ShowSuccess("The information has been saved successfully", "Saved");
                    }
                    catch(Exception ex)
                    {
                        toastService.ShowError("There was an error when trying to save", "Error");
                    }
                }
                else
                {
                    try
                    {
                        await EducationService.Update(personEducation);
                        personEducation = new Education();
                        await OnInitializedAsync();
                        edit = false;
                        this.StateHasChanged();
                        toastService.ShowSuccess("The information has been saved successfully", "Saved");                    
                    }
                    catch (Exception ex)
                    {
                        toastService.ShowError("There was an error when trying to save", "Error");
                    }

                }
            }
            else
            {
                toastService.ShowError("Please make sure that you fill all required field", "Error");
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
             await OnInitializedAsync();

        }


        protected async Task GetEducation(int id)
        {
            personEducation = await EducationService.Get(id);
            await OnInitializedAsync();
            edit = true;

        }

        protected async Task DeleteEducation(int id)
        {
            await EducationService.Delete(id);
            personEducation = new Education();
            await OnInitializedAsync();
            this.StateHasChanged();
         

            toastService.ShowWarning("Qualifcation is removed", "Warning");


        }
    }
}
