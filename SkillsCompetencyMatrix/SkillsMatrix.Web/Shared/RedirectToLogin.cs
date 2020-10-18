using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Shared
{
    public class RedirectToLogin: ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            NavigationManager.NavigateTo("/signin");
            return base.OnAfterRenderAsync(firstRender);
        }
    }
}
