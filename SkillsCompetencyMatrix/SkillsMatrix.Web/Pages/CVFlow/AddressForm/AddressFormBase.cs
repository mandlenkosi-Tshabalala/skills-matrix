
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Web.Services;
using Blazored.Toast.Services;
using System;

namespace SkillsMatrix.Web.Pages.CVFlow.AddressForm
{
    public class AddressFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IAddressService AddressService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }
        protected Address address = new Address();
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
                    editContext = new EditContext(address);

                    address = await AddressService.Get(user.Id);
                    if (address != null)
                    {
                        editContext = new EditContext(address);
                    }
                }
            }
        }

        protected async void HandleValidSubmit()
        {
            var isValid = editContext.Validate();

            if (isValid)
            {
                if (address != null)
                    address.UserId = UserId;

                if (address.Id == 0)
                {
                    try
                    {
                        await AddressService.Create(address);
                        NavigationManager.NavigateTo($"/personEducation");

                    }
                    catch
                    {
                        toastService.ShowError("There was an error when trying to save", "Error");
                    }

                    
                }
                else
                {
                    try
                    {

                        var result = await AddressService.Update(address);
                        NavigationManager.NavigateTo($"/personEducation");
                    }
                    catch(Exception ex)
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

        protected async void QuickSave()
        {
            var isValid = editContext.Validate();

            if (isValid)
            {
                if (address != null)
                    address.UserId = UserId;

                if (address.Id == 0)
                {
                    try
                    {
                        await AddressService.Create(address);
                        await  OnInitializedAsync();
                        toastService.ShowSuccess("The information has been saved successfully", "Saved");

                    }
                    catch
                    {
                        toastService.ShowError("There was an error when trying to save", "Error");
                    }
                }
                else
                {
                    try
                    {
                        
                        var result = await AddressService.Update(address);
                        await OnInitializedAsync();
                        toastService.ShowSuccess("The information has been saved successfully", "Saved");
                    }
                    catch
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
            NavigationManager.NavigateTo($"/personDetails");
        }


    }
}


