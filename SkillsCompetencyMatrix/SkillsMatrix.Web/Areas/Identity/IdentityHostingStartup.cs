using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillsMatrix.Web.Data;

[assembly: HostingStartup(typeof(SkillsMatrix.Web.Areas.Identity.IdentityHostingStartup))]
namespace SkillsMatrix.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SkillsMatrixWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SkillsMatrixWebContextConnection")));

                //require email confirmation
                services.AddDefaultIdentity<IdentityUser<int>>(options => { options.SignIn.RequireConfirmedEmail = true; options.SignIn.RequireConfirmedAccount = true; })
                    .AddRoles<IdentityRole<int>>().AddEntityFrameworkStores<SkillsMatrixWebContext>();
                //services.AddDefaultIdentity<IdentityUser<int>>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<SkillsMatrixWebContext>();

                services.AddScoped<IRoleStore<IdentityRole<int>>, RoleStore<IdentityRole<int>, SkillsMatrixWebContext, int>>();
            });
        }
    }
}