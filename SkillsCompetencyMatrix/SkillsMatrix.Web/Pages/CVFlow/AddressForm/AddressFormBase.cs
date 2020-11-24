
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Web.Services;
using Blazored.Toast.Services;
using System;
using Microsoft.JSInterop;
using SkillsMatrix.Web.Services.Shared;

namespace SkillsMatrix.Web.Pages.CVFlow.AddressForm
{
    public class AddressFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IAddressService AddressService { get; set; }

        [Inject]
        public IPersonService PersonService { get; set; }
        [Inject]
        public IPercentageCalc PercentageCalcService { get; set; }
        public int percentage = 0;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [Inject]
        IJSRuntime jsRuntime { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }
        protected Address address = new Address();
        protected EditContext editContext;
        private bool SetupDone { get; set; }
        private static Action<Address> AddressAction;

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

            if (!SetupDone)
            {
                SetupDone = true;
                await SetupAddress();
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
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
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
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
                        NavigationManager.NavigateTo($"/personEducation");
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
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
                        await OnInitializedAsync();
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
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
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

        private async Task SetupAddress()
        {
            await jsRuntime.InvokeVoidAsync("DoScriptSetup");
        }

        protected override void OnInitialized()
        {
            AddressAction = UpdateAddress;
        }

        private void UpdateAddress(Address newAddress)
        {
            address.StreetNumber = newAddress.StreetNumber;
            address.StreetName = newAddress.StreetName;
            address.City = newAddress.City;
            address.State = newAddress.State;
            address.ZipCode = newAddress.ZipCode;
            address.Country = newAddress.Country;
            StateHasChanged();
        }

        [JSInvokable]
        public static void UpdateAddressCaller(string Address)
        {
            Address newAddress = System.Text.Json.JsonSerializer.Deserialize<Address>(Address);
            AddressAction.Invoke(newAddress);
        }
    }
}


