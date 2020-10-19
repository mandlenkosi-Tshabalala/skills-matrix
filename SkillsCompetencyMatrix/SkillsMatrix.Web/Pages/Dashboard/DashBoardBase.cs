using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Pages.Dashboard
{
    public class DashBoardBase : ComponentBase
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            ClaimsPrincipal user = (await AuthState).User;

            if (user.Identity.IsAuthenticated)
            {
                Console.WriteLine($"{user.Identity.Name} is authenticated.");
            }
        }
    }
}
