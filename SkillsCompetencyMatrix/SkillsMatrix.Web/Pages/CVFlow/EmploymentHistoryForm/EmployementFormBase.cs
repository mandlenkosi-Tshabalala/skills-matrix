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

namespace SkillsMatrix.Web.Pages.CVFlow.EmploymentHistoryForm
{
    public class EmployementFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IEmployementHistoryService EmployementService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }

        protected List<Employment> personEmployments = new List<Employment>();

        protected Employment personEmployment = new Employment();

        protected EditContext editContext;

        private bool edit = false;

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(personEmployment);

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;


                    personEmployments = await EmployementService.GetEmployment(user.Id);


                }
            }

        }

        protected async void HandleValidSubmit()
        {

            var isValid = editContext.Validate();

            if (isValid)
            {
                if (edit == false)
                {
                    try
                    {
                    personEmployment.UserId = UserId;
                    await EmployementService.Create(personEmployment);
                    personEmployment = new Employment();
                    await OnInitializedAsync();
                        this.StateHasChanged();
                    toastService.ShowSuccess("The information has been saved successfully", "Saved");
                    }
                    catch (Exception ex)
                    {
                        toastService.ShowError("There was an error when trying to save", "Error");
                    }
                }
                else
                {
                    try
                    {
                        await EmployementService.Update(personEmployment);
                    personEmployment = new Employment();
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
            NavigationManager.NavigateTo($"/personEducation");
        }

        protected void Next()
        {
            NavigationManager.NavigateTo("/expertise");
        }

        protected async void Cancel()
        {
            await OnInitializedAsync();
        }


        protected async Task GetEmployment(int id)
        {
            personEmployment = await EmployementService.Get(id);
            await OnInitializedAsync();
            edit = true;

        }

        protected async Task DeleteEmployment(int id)
        {
            await EmployementService.Delete(id);
            personEmployment = new Employment();
            await OnInitializedAsync();   
            toastService.ShowWarning("Empolyment history is removed", "Warning");
            this.StateHasChanged();
        }
    }
}
