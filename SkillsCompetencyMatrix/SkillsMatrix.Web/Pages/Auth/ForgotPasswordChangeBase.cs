using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;

using NETCore.MailKit.Core;
using MailKit.Net.Smtp;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace SkillsMatrix.Web.Pages.Auth
{
    public class ForgotPasswordChangeBase : ComponentBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] IHostEnvironmentAuthenticationStateProvider HostAuthentication { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> _userManager { get; set; }
        [Inject]
        protected SignInManager<IdentityUser<int>> _signInManager { get; set; }
        [Inject]
        protected ILogger<RegisterBase> _logger { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public IEmailService _emailService { get; set; }

        //protected InputModel Input = new InputModel();

        public class UserLoginDetails
        {
            public string Password { set; get; }
            public string PasswordConfirm { set; get; }
        }
        protected UserLoginDetails userLoginDetails = new UserLoginDetails();


        public string userID { get; set; }
        public string token { get; set; }

        public bool EmailSent = false;

        public async void HandleForgotPasswordChange()
        {
            _logger.LogInformation("HandleForgotPasswordChange started...");

            


            var user = await _userManager.FindByIdAsync(userID);
            //token = HttpUtility.UrlDecode(token);
            //token = token.Replace(' ', '+');
            token = System.Web.HttpUtility.UrlDecode(token);
            var result = await _userManager.ResetPasswordAsync(user, token, userLoginDetails.Password);

            if (result.Succeeded)
            {
                _navigationManager.NavigateTo("/passwordChanged");
            }
            else
            {

            }
        }


        protected override async Task OnInitializedAsync()
        {
            _logger.LogInformation("OnInitializedAsync started...");

            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("userId", out var _userId))
            {
                userID = Convert.ToString(_userId);
            }

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var _token))
            {
                token = Convert.ToString(_token);
            }
            

            if (true)
            {


                //NavigationManager.NavigateTo("/signin");
            }
            else
            {
                //Logger.LogWarning($"Username {userLoginDetails.Email} or password {userLoginDetails.Password} is incorrect.");
            }

        }
    }
}
