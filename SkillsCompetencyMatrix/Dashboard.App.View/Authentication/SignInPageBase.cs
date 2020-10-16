using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Authentication
{
    public class SignInPageBase : ComponentBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] IHostEnvironmentAuthenticationStateProvider HostAuthentication { get; set; }

        [Inject]
        protected UserManager<IdentityUser> UserManager { get; set; }
        [Inject]
        protected SignInManager<IdentityUser> SignInManager { get; set; }
        [Inject]
        protected ILogger<SignInPageBase> Logger { get; set; }
        [Inject]
        protected IEmailSender EmailSender { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string Email { set; get; }

        public string Password { set; get; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        public void HandleEmailChange(ChangeEventArgs arg)
        {
            Email = arg.Value.ToString();

            StateHasChanged();
        }

        public void HandlePasswordChange(ChangeEventArgs arg)
        {
            Password = arg.Value.ToString();

            StateHasChanged();
        }

        public async void HandleSignIn()
        {
            var user = await UserManager.FindByNameAsync(Email);
            var valid = await SignInManager.UserManager.CheckPasswordAsync(user, Password);

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

               // NavigationManager.NavigateTo("/", true);

            }
            else
            {
                // successMessage = "";
                // errorMessage = "Username or password is incorrect.";
            }

            //System.Console.WriteLine("HandleSignIn");

            //_ = ScriptHelpers.GoBackAsync();
            //use navigation manager

            // System.Console.WriteLine("HandleSignInDone");
        }

        protected override void OnAfterRender(bool firstRender)
        {
            NavigationManager.NavigateTo("/dashboard");
        }

        private async void CreateUser()
        {
            var user = new IdentityUser { UserName = Email, Email = Email };
            var result = await UserManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                Logger.LogInformation("User created a new account with password.");

                await SignInManager.SignInAsync(user, isPersistent: false);
            }
        }
    }
}
