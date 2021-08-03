using Blazored.Toast.Services;
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
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;


namespace SkillsMatrix.Web.Auth
{
    public class SignInPageBase : ComponentBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] IHostEnvironmentAuthenticationStateProvider HostAuthentication { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }
        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }
        [Inject]
        protected ILogger<SignInPageBase> Logger { get; set; }
        [Inject]
        protected IEmailSender EmailSender { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IToastService toastService { get; set; }

        protected User User { get; set; }

        protected EditContext editContext;

        public class UserLoginDetails
        {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress]
            public string Email { set; get; }
            [Required(ErrorMessage = "Password is required.")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password, ErrorMessage = "Password not valid.")]
            public string Password { set; get; }
        }

        protected UserLoginDetails userLoginDetails = new UserLoginDetails();

        protected override Task OnInitializedAsync()
        {
            Logger.LogInformation("OnInitializedAsync started...");
            editContext = new EditContext(userLoginDetails);

            return base.OnInitializedAsync();
        }

        public void HandleEmailChange(ChangeEventArgs arg)
        {
            userLoginDetails.Email = arg.Value.ToString();
            StateHasChanged();
        }

        public void HandlePasswordChange(ChangeEventArgs arg)
        {
            userLoginDetails.Password = arg.Value.ToString();
            StateHasChanged();
        }

        public async void HandleSignIn()
        {
            
                Logger.LogInformation("HandleSignIn started...");
            var user = await UserManager.FindByNameAsync(userLoginDetails.Email);

            if (user!=null && await UserManager.IsEmailConfirmedAsync(user))
            {
                var valid = await SignInManager.UserManager.CheckPasswordAsync(user, userLoginDetails.Password);

                if (valid)
                {
                    var principal = await SignInManager.CreateUserPrincipalAsync(user);

                    var identity = new ClaimsIdentity(
                        principal.Claims,
                        Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    principal = new System.Security.Claims.ClaimsPrincipal(identity);
                    SignInManager.Context.User = principal;
                    HostAuthentication.SetAuthenticationState(Task.FromResult(new AuthenticationState(principal)));

                    // now the authState is updated
                    var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    toastService.ShowError($"Username or password is incorrect.");
                    Logger.LogWarning($"Username {userLoginDetails.Email} or password {userLoginDetails.Password} is incorrect.");
                }
            }

            else
            {
                toastService.ShowError($"Email {userLoginDetails.Email} is not confirmed.");
                Logger.LogWarning($"Email {userLoginDetails.Email} is not confirmed.");
            }

            
        }

        private async void CreateUser()
        {
            var user = new IdentityUser<int> { UserName = userLoginDetails.Email, Email = userLoginDetails.Email };
            var result = await UserManager.CreateAsync(user, userLoginDetails.Password);
            if (result.Succeeded)
            {
                Logger.LogInformation("User created a new account with password.");

                var principal = await SignInManager.CreateUserPrincipalAsync(user);

                var identity = new ClaimsIdentity(
                    principal.Claims,
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme
                );
                principal = new System.Security.Claims.ClaimsPrincipal(identity);
                SignInManager.Context.User = principal;
                HostAuthentication.SetAuthenticationState(Task.FromResult(new AuthenticationState(principal)));

                // now the authState is updated
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

                NavigationManager.NavigateTo("/");
            }
        }
    }
}
