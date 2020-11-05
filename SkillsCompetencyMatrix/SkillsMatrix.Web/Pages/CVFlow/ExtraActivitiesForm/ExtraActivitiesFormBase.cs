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
using System.Diagnostics;

namespace SkillsMatrix.Web.Pages.CVFlow.ExtraActivitiesForm
{
    public class ExtraActivitiesFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IActivityService ActivityService { get; set; }

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

        protected UserActivities activity = new UserActivities();

        protected List<UserActivities> activities = new List<UserActivities>();

        protected EditContext editContext;


        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(activity);

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;


                    activities = await ActivityService.GetActivity(user.Id);


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
                activity.UserId = UserId;
                await ActivityService.Create(activity);
                await OnInitializedAsync();
                NavigationManager.NavigateTo($"/extraactivities");
                activity = new UserActivities();
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
                await ActivityService.Update(activity);
                await OnInitializedAsync();
                edit = false;
                NavigationManager.NavigateTo($"/extraactivities");
                activity = new UserActivities();
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
            NavigationManager.NavigateTo($"/skills");
        }

        protected void Next()
        {
            NavigationManager.NavigateTo("/ViewCV");
        }

        protected async void Cancel()
        {
            activity = new UserActivities();
            NavigationManager.NavigateTo($"/extraactivities");

        }


        protected async Task GetActivity(int id)
        {
            activity = await ActivityService.Get(id);
            edit = true;

        }

        protected async Task DeleteActivity(int id)
        {
            await ActivityService.Delete(id);

            await OnInitializedAsync();
            NavigationManager.NavigateTo($"/extraactivities");
            activity = new UserActivities();
            toastService.ShowWarning("Activity is removed", "Warning");

        }
    }
}