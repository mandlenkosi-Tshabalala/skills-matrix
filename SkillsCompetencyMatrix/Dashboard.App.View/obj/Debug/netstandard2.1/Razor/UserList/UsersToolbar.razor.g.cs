#pragma checksum "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\UserList\UsersToolbar.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f2ffdbb2ef786880a8e7b9cc8649e95748b9bb30"
// <auto-generated/>
#pragma warning disable 1591
namespace Skclusive.Blazor.Dashboard.App.View.UserList
{
    #line hidden
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using System.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.Common;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.Home;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.Layout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.Settings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.UserList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.ProductList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.OrderList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.Chart;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.Resource;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Blazor.Dashboard.App.View.Logo;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Core.Component;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Transition.Component;

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 26 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Component;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Theme;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Form;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Input;

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Text;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Typography;

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Progress;

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Transition;

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Button;

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Icon;

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Paper;

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Avatar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Badge;

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Selection;

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Toolbar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.AppBar;

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Divider;

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Grid;

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Table;

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Baseline;

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Card;

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Hidden;

#line default
#line hidden
#nullable disable
#nullable restore
#line 48 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.List;

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Modal;

#line default
#line hidden
#nullable disable
#nullable restore
#line 50 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Script;

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Drawer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Dialog;

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Container;

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Tab;

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Script.DomHelpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using Skclusive.Material.Layout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using ChartJs.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using ChartJs.Blazor.Charts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using ChartJs.Blazor.ChartJS.PieChart;

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using ChartJs.Blazor.ChartJS.Common.Properties;

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\_Imports.razor"
using ChartJs.Blazor.Util;

#line default
#line hidden
#nullable disable
    public partial class UsersToolbar : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "users-toolbar-root");
            __builder.AddMarkupContent(2, "\r\n    ");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "users-toolbar-row");
            __builder.AddMarkupContent(5, "\r\n        <span class=\"users-toolbar-spacer\"></span>\r\n        ");
            __builder.OpenComponent<Skclusive.Material.Button.Button>(6);
            __builder.AddAttribute(7, "class", "users-toolbar-import-button");
            __builder.AddAttribute(8, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Skclusive.Core.Component.IComponentContext>)((context) => (__builder2) => {
                __builder2.AddContent(9, "Import");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(10, "\r\n        ");
            __builder.OpenComponent<Skclusive.Material.Button.Button>(11);
            __builder.AddAttribute(12, "class", "users-toolbar-export-button");
            __builder.AddAttribute(13, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Skclusive.Core.Component.IComponentContext>)((context) => (__builder2) => {
                __builder2.AddContent(14, "Export");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(15, "\r\n        ");
            __builder.OpenComponent<Skclusive.Material.Button.Button>(16);
            __builder.AddAttribute(17, "Color", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Core.Component.Color>(
#nullable restore
#line 7 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\UserList\UsersToolbar.razor"
                    Color.Primary

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(18, "Variant", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Material.Button.ButtonVariant>(
#nullable restore
#line 8 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\UserList\UsersToolbar.razor"
                      ButtonVariant.Contained

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(19, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Skclusive.Core.Component.IComponentContext>)((context) => (__builder2) => {
                __builder2.AddMarkupContent(20, "\r\n            Add\r\n        ");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(21, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n    ");
            __builder.OpenElement(23, "div");
            __builder.AddAttribute(24, "class", "users-toolbar-row");
            __builder.AddMarkupContent(25, "\r\n        ");
            __builder.OpenComponent<Skclusive.Blazor.Dashboard.App.View.Common.SearchInput>(26);
            __builder.AddAttribute(27, "Class", "users-toolbar-search-input");
            __builder.AddAttribute(28, "Placeholder", "Search user");
            __builder.CloseComponent();
            __builder.AddMarkupContent(29, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(30, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
