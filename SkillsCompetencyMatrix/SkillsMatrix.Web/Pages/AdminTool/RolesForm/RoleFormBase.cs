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

namespace SkillsMatrix.Web.Pages.AdminTool.RolesForm
{
    public class RoleFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IRolesService roleService { get; set; }

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
        protected Role role = new Role();
        protected List<Role> roles = new List<Role>();
        protected EditContext editContext;

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(role);

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;


                    roles = await roleService.GetAll();


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
                        await roleService.Create(role);
                        await OnInitializedAsync();
                        NavigationManager.NavigateTo($"/adminRole");
                        role = new Role();
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
                        await roleService.Update(role);
                        await OnInitializedAsync();
                        edit = false;
                        NavigationManager.NavigateTo($"/adminRole");
                        role = new Role();
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
        protected async Task GetRole(int id)
        {
            role = await roleService.Get(id);
            edit = true;

        }

        protected async Task DeleteRole(int id)
        {
            await roleService.Delete(id);

            await OnInitializedAsync();
            NavigationManager.NavigateTo($"/adminRole");
            role = new Role();
            toastService.ShowWarning("Role is removed", "Warning");

        }

        protected void ManageRole(int roleId)
        {

            NavigationManager.NavigateTo($"/userrole/" + roleId.ToString());
        }
    }
}