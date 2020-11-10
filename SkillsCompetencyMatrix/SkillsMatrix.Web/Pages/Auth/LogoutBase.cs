using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Pages.Auth
{
    public class LogoutBase : ComponentBase
    {
        [Inject]
        protected SignInManager<IdentityUser<int>> _signInManager { get; set; }
        [Inject]
        protected ILogger<LogoutBase> _logger { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        //protected override Task OnInitializedAsync()
        //{
        //    _signInManager.SignOutAsync();

        //    _logger.LogInformation("User logged out.");
        //    _navigationManager.NavigateTo("/signin");

        //    return base.OnInitializedAsync();
        //}

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");
            _navigationManager.NavigateTo("/signin");
            this.StateHasChanged();
            return base.OnAfterRenderAsync(firstRender);

          
        }
    }
}
