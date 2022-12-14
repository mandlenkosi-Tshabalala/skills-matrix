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

namespace SkillsMatrix.Web.Pages.CVFlow.ProfessionalMembershipForm
{
    public class ProfessionalMembershipFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IProfessionalMembershipService ProfessionalMembershipService { get; set; }

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

        protected Membership membership = new Membership();

        protected List<Membership> memberships = new List<Membership>();

        protected EditContext editContext;


        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(membership);

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;


                    memberships = await ProfessionalMembershipService.GetAll(user.Id);


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
                 membership.UserId = UserId;
                await ProfessionalMembershipService.Create(membership);
                await OnInitializedAsync();
                NavigationManager.NavigateTo($"/membership");
                membership = new Membership();
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
                await ProfessionalMembershipService.Update(membership);
                await OnInitializedAsync();
                edit = false;
                NavigationManager.NavigateTo($"/membership");
                membership = new Membership();
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
            NavigationManager.NavigateTo($"/personCompetencies");
        }

        protected void Next()
        {
            NavigationManager.NavigateTo("/skills");
        }

        protected async void Cancel()
        {
            membership = new Membership();
            NavigationManager.NavigateTo($"/membership");

        }


        protected async Task GetMembership(int id)
        {
            membership = await ProfessionalMembershipService.Get(id);
            await OnInitializedAsync();
            edit = true;

        }

        protected async Task DeleteMembership(int id)
        {
            await ProfessionalMembershipService.Delete(id);

            await OnInitializedAsync();
            NavigationManager.NavigateTo($"/membership");
            membership = new Membership();
            toastService.ShowWarning("Membership is removed", "Warning");

        }
    }
}