#pragma checksum "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fa47e614e37627f899e2fff2a484ab478d2cedfd"
// <auto-generated/>
#pragma warning disable 1591
namespace Skclusive.Blazor.Dashboard.App.View.Account
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
    public partial class AccountProfile : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Skclusive.Material.Card.Card>(0);
            __builder.AddAttribute(1, "Class", "account-profile-root");
            __builder.AddAttribute(2, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(3, "\r\n    ");
                __builder2.OpenComponent<Skclusive.Material.Card.CardContent>(4);
                __builder2.AddAttribute(5, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(6, "\r\n        ");
                    __builder3.OpenElement(7, "div");
                    __builder3.AddAttribute(8, "class", "account-profile-details");
                    __builder3.AddMarkupContent(9, "\r\n            ");
                    __builder3.OpenElement(10, "div");
                    __builder3.AddMarkupContent(11, "\r\n                ");
                    __builder3.OpenComponent<Skclusive.Material.Typography.Typography>(12);
                    __builder3.AddAttribute(13, "GutterBottom", true);
                    __builder3.AddAttribute(14, "Variant", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Material.Typography.TypographyVariant>(
#nullable restore
#line 7 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                              TypographyVariant.H2

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(15, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(16, "\r\n                    John Doe\r\n                ");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(17, "\r\n                ");
                    __builder3.OpenComponent<Skclusive.Material.Typography.Typography>(18);
                    __builder3.AddAttribute(19, "Class", "account-profile-location-text");
                    __builder3.AddAttribute(20, "Color", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Core.Component.Color>(
#nullable restore
#line 12 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                            Color.TextSecondary

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(21, "Variant", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Material.Typography.TypographyVariant>(
#nullable restore
#line 13 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                              TypographyVariant.Body1

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(22, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(23, "\r\n                    ");
                        __builder4.AddContent(24, 
#nullable restore
#line 14 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                     User.City

#line default
#line hidden
#nullable disable
                        );
                        __builder4.AddContent(25, ", ");
                        __builder4.AddContent(26, 
#nullable restore
#line 14 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                                 User.Country

#line default
#line hidden
#nullable disable
                        );
                        __builder4.AddMarkupContent(27, "\r\n                ");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(28, "\r\n                ");
                    __builder3.OpenComponent<Skclusive.Material.Typography.Typography>(29);
                    __builder3.AddAttribute(30, "Class", "account-profile-date-text");
                    __builder3.AddAttribute(31, "Color", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Core.Component.Color>(
#nullable restore
#line 18 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                            Color.TextSecondary

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(32, "Variant", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Material.Typography.TypographyVariant>(
#nullable restore
#line 19 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                              TypographyVariant.Body1

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(33, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddMarkupContent(34, "\r\n                    ");
                        __builder4.AddContent(35, 
#nullable restore
#line 20 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                     DateTime.Now.ToString("hh:mm A")

#line default
#line hidden
#nullable disable
                        );
                        __builder4.AddContent(36, " (");
                        __builder4.AddContent(37, 
#nullable restore
#line 20 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                                                        User.Timezone

#line default
#line hidden
#nullable disable
                        );
                        __builder4.AddMarkupContent(38, ")\r\n                ");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(39, "\r\n            ");
                    __builder3.CloseElement();
                    __builder3.AddMarkupContent(40, "\r\n            ");
                    __builder3.OpenComponent<Skclusive.Material.Avatar.Avatar>(41);
                    __builder3.AddAttribute(42, "Class", "account-profile-avatar");
                    __builder3.AddAttribute(43, "Src", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 25 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                      User.Avatar

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(44, "\r\n        ");
                    __builder3.CloseElement();
                    __builder3.AddMarkupContent(45, "\r\n        ");
                    __builder3.OpenElement(46, "div");
                    __builder3.AddAttribute(47, "class", "account-profile-progress");
                    __builder3.AddMarkupContent(48, "\r\n            ");
                    __builder3.OpenComponent<Skclusive.Material.Typography.Typography>(49);
                    __builder3.AddAttribute(50, "Variant", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Material.Typography.TypographyVariant>(
#nullable restore
#line 28 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                                  TypographyVariant.Body1

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(51, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.AddContent(52, "Profile Completeness: 70%");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(53, "\r\n            ");
                    __builder3.OpenComponent<Skclusive.Material.Progress.LinearProgress>(54);
                    __builder3.AddAttribute(55, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Decimal>(
#nullable restore
#line 30 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                       70

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(56, "Variant", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Material.Progress.LinearProgressVariant>(
#nullable restore
#line 31 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                          LinearProgressVariant.Determinate

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(57, "\r\n        ");
                    __builder3.CloseElement();
                    __builder3.AddMarkupContent(58, "\r\n    ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(59, "\r\n    ");
                __builder2.OpenComponent<Skclusive.Material.Divider.Divider>(60);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(61, "\r\n    ");
                __builder2.OpenComponent<Skclusive.Material.Card.CardActions>(62);
                __builder2.AddAttribute(63, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(64, "\r\n    ");
                    __builder3.OpenComponent<Skclusive.Material.Button.Button>(65);
                    __builder3.AddAttribute(66, "Class", "account-profile-upload-button");
                    __builder3.AddAttribute(67, "Color", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Core.Component.Color>(
#nullable restore
#line 38 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                Color.Primary

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(68, "Variant", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Material.Button.ButtonVariant>(
#nullable restore
#line 39 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                  ButtonVariant.Text

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(69, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Skclusive.Core.Component.IComponentContext>)((context) => (__builder4) => {
                        __builder4.AddMarkupContent(70, "\r\n        Upload picture\r\n    ");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(71, "\r\n        ");
                    __builder3.OpenComponent<Skclusive.Material.Button.Button>(72);
                    __builder3.AddAttribute(73, "Variant", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Skclusive.Material.Button.ButtonVariant>(
#nullable restore
#line 42 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
                          ButtonVariant.Text

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(74, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Skclusive.Core.Component.IComponentContext>)((context) => (__builder4) => {
                        __builder4.AddContent(75, "Remove picture");
                    }
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(76, "\r\n    ");
                }
                ));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(77, "\r\n");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 47 "C:\dev\SkillsCompetencyMatrix\Dashboard.App.View\Account\AccountProfile.razor"
 
    private (string Name, string City, string Country, string Timezone, string Avatar) User = (
        Name: "Shen Zhi",
        City: "Los Angeles",
        Country: "USA",
        Timezone: "GTM-7",
        Avatar: "./_content/Skclusive.Blazor.Dashboard.App.View/images/avatars/avatar_11.png"
    );

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
