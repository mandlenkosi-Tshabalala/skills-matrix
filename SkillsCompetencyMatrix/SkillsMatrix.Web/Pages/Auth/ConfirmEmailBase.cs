using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Pages.Auth
{
    public class ConfirmEmailBase : ComponentBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] IHostEnvironmentAuthenticationStateProvider HostAuthentication { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }
        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }
        [Inject]
        protected ILogger<ConfirmEmailBase> Logger { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        public class UserLoginDetails
        {
            public string Email { set; get; }
            public string Password { set; get; }
            public string Token { set; get; }
        }

        protected UserLoginDetails userLoginDetails = new UserLoginDetails();
        protected override async Task OnInitializedAsync()
        {
            Logger.LogInformation("OnInitializedAsync started...");

            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("userId", out var _userId))
            {
                userLoginDetails.Email = Convert.ToString(_userId);
            }

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var _token))
            {
                userLoginDetails.Token = Convert.ToString(_token);
            }

            var user =await  UserManager.FindByIdAsync(userLoginDetails.Email);
            //userLoginDetails.Token = System.Web.HttpUtility.UrlDecode(userLoginDetails.Token);
            var result =await UserManager.ConfirmEmailAsync(user, userLoginDetails.Token);


            if (result.Succeeded)
            {
               

                //NavigationManager.NavigateTo("/signin");
            }
            else
            {
                Logger.LogWarning($"Username {userLoginDetails.Email} or password {userLoginDetails.Password} is incorrect.");
            }

        }

    }
}
