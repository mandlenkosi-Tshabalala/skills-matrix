using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SkillsMatrix.Api.Models;

namespace SkillsMatrix.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICompetencyRepository, CompetencyRepository>();
            services.AddScoped<ICompetencyCategoryRepository, CompetencyCategoryRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IEmploymentHistoryRepository, EmploymentHistoryRepository>();
            services.AddScoped<IExpertiseRepository, ExpertiseRepository>();
            services.AddScoped<IIndustryRepository, IndustryRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProfessionalMembershipRepository, ProfessionalMembershipRepository>();
            services.AddScoped<ISkillsRepository, SkillsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddScoped<IUserSkillsRepository, UserSkillsRepository>();
            services.AddScoped<IUserExpertiseRepository, UserExpertiseRepository>();
            services.AddScoped<IPersonCompetenciesRepository, PersonCompetenciesRepository>();
            services.AddScoped<IProfessionalMembershipRepository, ProfessionalMembershipRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();

            services.AddControllers();

            services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
