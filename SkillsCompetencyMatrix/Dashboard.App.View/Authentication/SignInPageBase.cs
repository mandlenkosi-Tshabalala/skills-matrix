using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.Authentication
{
    public class SignInPageBase : ComponentBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<SignInPageBase> _logger;

        public string Email { set; get; }

        public string Password { set; get; }

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

        public void HandleSignIn()
        {
            var result = _signInManager.PasswordSignInAsync(Email, Password, false, lockoutOnFailure: false);
            if (result.Result.Succeeded)
            {

            }

            System.Console.WriteLine("HandleSignIn");

            //_ = ScriptHelpers.GoBackAsync();
            //use navigation manager

            System.Console.WriteLine("HandleSignInDone");
        }
    }
}
