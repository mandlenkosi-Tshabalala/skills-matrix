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

namespace SkillsMatrix.Web.Pages.Auth
{
    public class RegisterBase : ComponentBase
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

        protected InputModel Input = new InputModel();

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        protected async void HandleValidSubmit()
        {
            var user = new IdentityUser<int> { 
                UserName = Input.Email, 
                Email = Input.Email, 
                EmailConfirmed = true 
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                //create userrole
                _logger.LogInformation("User created a new account with password.");

                var principal = await _signInManager.CreateUserPrincipalAsync(user);

                var identity = new ClaimsIdentity(
                    principal.Claims,
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme
                );

                principal = new ClaimsPrincipal(identity);

                _signInManager.Context.User = principal;

                HostAuthentication.SetAuthenticationState(Task.FromResult(new AuthenticationState(principal)));

                // now the authState is updated
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

                _navigationManager.NavigateTo("/");
            }
        }
    }
}
