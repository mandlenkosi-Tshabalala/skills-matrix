
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Skclusive.Blazor.Dashboard.App.View.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SkillsMatrix.Web.Pages.CVFlow.AddressForm
{
    public class AddressFormBase : ComponentBase
    {

        [Inject]
        public IAddressService AddressService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string PersonId { get; set; }

        protected Address address = new Address();

        protected EditContext editContext;

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ClaimsPrincipal user = (await AuthState).User;

            if (user.Identity.IsAuthenticated)
            {
                editContext = new EditContext(address);

                address = await AddressService.Get(1);

                if (address != null)
                {
                    editContext = new EditContext(address);
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            ClaimsPrincipal user = (await AuthState).User;

            if (user.Identity.IsAuthenticated)
            {
                Console.WriteLine($"{user.Identity.Name} is authenticated.");
            }
        }

        protected void HandleValidSubmit()
        {
            if (address != null)
                address.UserId = int.Parse(PersonId);

            //Check if its a new record 
            if (address.Id == 0)
            {
                var result = AddressService.Create(address);
                if (result == null)
                {
                    //Load Spinner
                }
                else
                {
                    NavigationManager.NavigateTo($"/personEducation/{PersonId}");
                }
            }
            else
            {
                AddressService.Update(address);
                NavigationManager.NavigateTo($"/personEducation/{PersonId}");
            }
        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/personDetails/{PersonId}");
        }
    }
}
