
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Web.Services;

namespace SkillsMatrix.Web.Pages.CVFlow.AddressForm
{
    public class AddressFormBase : ComponentBase
    {

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
            if (address != null)
                address.UserId = UserId;

            //Check if its a new record 
            if (address.Id == 0)
            {
                var result = await AddressService.Create(address);
                if (result == null)
                {
                    //Load Spinner
                }
                else
                {
                    NavigationManager.NavigateTo($"/personEducation");
                }
            }
            else
            {
               await AddressService.Update(address);
                NavigationManager.NavigateTo($"/personEducation");
            }
        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/personDetails");
        }
    }
}
