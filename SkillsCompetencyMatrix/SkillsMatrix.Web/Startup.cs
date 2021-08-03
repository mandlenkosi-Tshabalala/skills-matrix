using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using SkillsMatrix.Web.Data;
using SkillsMatrix.Web.Services;
using SkillsMatrix.Web.Services.Shared;
using SkillsMatrix.Web.Shared;

namespace SkillsMatrix.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string url = Configuration.GetValue<String>("ServiceBaseUrl");
            services.AddAuthentication("Identity.Application").AddCookie();
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddRazorPages();
            services.AddServerSideBlazor();
            

            services.AddHttpClient<IPersonService, PersonService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<IEducationService, EducationService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<IAddressService, AddressService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<IProfessionalMembershipService, ProfessionalMembershipService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<IEmployementHistoryService, EmployementHistoryService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<IPersonExpertiseService, PersonExpertiseService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<IPersonCompetencies, PersonCompetencies>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<IExpertiseService, ExpertiseService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<ISkillsService, SkillsService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<ICompetenciesService, CompetenciesService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<ICompetenciesCategoryService, CompetenciesCategoryService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<IActivityService, ActivityService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddHttpClient<IRolesService, RolesService>(client =>
            {
                client.BaseAddress = new Uri(url);
            });

            services.AddBlazoredToast();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            
            services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp =>
            {
                var provider = (ServerAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>();
                return provider;
            });
  
            //    services.AddDefaultIdentity<IdentityUser>()
            //.AddRoles<IdentityRole>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            services.AddScoped<IPercentageCalc, PercentageCalc>();


            var mailKitOptions = Configuration.GetSection("Email").Get<MailKitOptions>();
            services.AddMailKit(config => {
                config.UseMailKit(mailKitOptions);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
