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
using Microsoft.AspNetCore.Components.Forms;
using Blazored.Toast.Services;

namespace SkillsMatrix.Web.Pages.Auth
{
    public class PasswordChangeBase : ComponentBase
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
        [Inject]
        public IToastService toastService { get; set; }
        //protected InputModel Input = new InputModel();

        public class UserLoginDetails
        {
            [Display(Name = "Old password")]
            public string OldPassword { set; get; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Password must meet requirements")]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string PasswordConfirm { set; get; }

            public string Email { set; get; }
            public string Token { set; get; }
        }
        protected UserLoginDetails userLoginDetails = new UserLoginDetails();
        protected EditContext editContext;

        public string userID { get; set; }
        public string token { get; set; }

        public bool EmailSent = false;

        public async void HandlePasswordChange()
        {
            _logger.LogInformation("PasswordChange started...");


            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("Email", out var _userId))
            {
                userLoginDetails.Email = Convert.ToString(_userId);
            }

            //if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var _token))
            //{
            //    userLoginDetails.Token = Convert.ToString(_token);
            //}

            var user = await _userManager.FindByEmailAsync(userLoginDetails.Email);
            //token = HttpUtility.UrlDecode(token);
            //token = token.Replace(' ', '+');
            token = System.Web.HttpUtility.UrlDecode(token);
            var result = await _userManager.ChangePasswordAsync(user, userLoginDetails.OldPassword, userLoginDetails.Password);

            if (result.Succeeded)
            {
                _navigationManager.NavigateTo("/passwordChanged");
            }
            else
            {
                toastService.ShowError("An error occured while changing the password.");
            }
        }


        protected override async Task OnInitializedAsync()
        {
            _logger.LogInformation("OnInitializedAsync started...");
            editContext = new EditContext(userLoginDetails);
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
