using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SkillsMatrix.Models;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Pages
{
    public class AddressListBase : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public IAddressService AddressService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public IEnumerable<Address> Addresses { get; set; }

        [Inject]
        public IAuthorizationService AuthorizationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"url");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            // check role
            if (authenticationState.User.IsInRole("Administrator"))
            {
                // execute
            }

            var user = (await authenticationStateTask).User;
            if ((await AuthorizationService.AuthorizeAsync(user, "admin-policy"))
                .Succeeded)
            {
                // execute
            }

            Addresses = (await AddressService.GetAddresses()).ToList();
        }
    }
}
