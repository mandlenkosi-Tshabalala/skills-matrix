using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Shared
{
    public class LogoutRedirect : ComponentBase
    {
        [Inject]
        protected SignInManager<IdentityUser<int>> _signInManager { get; set; }
        [Inject]
        protected ILogger<LogoutRedirect> _logger { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected override void OnInitialized()
        {
            _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");
            _navigationManager.NavigateTo("/signin");

            base.OnInitialized();
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //   await _signInManager.SignOutAsync();

        //    _logger.LogInformation("User logged out.");
        //    _navigationManager.NavigateTo("/signin");

        //    return base.OnAfterRenderAsync(firstRender);
        //}
    }
}
