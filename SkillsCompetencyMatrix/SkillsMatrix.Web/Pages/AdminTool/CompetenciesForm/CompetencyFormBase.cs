
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Web.Services;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;

namespace SkillsMatrix.Web.Pages.AdminTool
{
    public class CompetencyFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public ICompetenciesCategoryService CompetenciesCategoryService { get; set; }

        [Inject]
        public ICompetenciesService CompetenciesService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private bool edit = false;

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }
        protected Competency competency = new Competency();
        protected List<Competency> competencies = new List<Competency>();

        protected CompetencyCategory competencyCategory = new CompetencyCategory();
        protected List<CompetencyCategory> competencyCategories = new List<CompetencyCategory>();

        protected int competencyCategoryID { get; set; }

        protected EditContext editContext;

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(competency);

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                   // competencies = await CompetenciesService.GetAll();
                    competencyCategories = await CompetenciesCategoryService.GetAll();
                }
            }

        }

        protected async void HandleCategory()
        {


            if (edit == false)
            {

                await CompetenciesCategoryService.Create(competencyCategory);
                await OnInitializedAsync();
                NavigationManager.NavigateTo($"/adminCompetency");
                competencyCategory = new CompetencyCategory();

            }
            else
            {
                await CompetenciesCategoryService.Update(competencyCategory);
                await OnInitializedAsync();
                edit = false;
                NavigationManager.NavigateTo($"/adminCompetency");
                competencyCategory = new CompetencyCategory();


            }

        }

        protected async void HandleCompetency()
        {
            if (edit == false)
            {
                competency.CatagoryId = competencyCategoryID;
                await CompetenciesService.Create(competency);
                competencies =  await CompetenciesService.GetAll(competencyCategoryID);
                competency.Name = "";
                this.StateHasChanged();
            }
            else
            {
                await CompetenciesService.Update(competency);
                competencies = await CompetenciesService.GetAll(competencyCategoryID);
                competency = new Competency();
                edit = false;
                this.StateHasChanged();
            }

        }

        protected async void SearchCompetency(ChangeEventArgs e)
        {
             competencyCategoryID = (Convert.ToInt32(e.Value));
             competencies = await  CompetenciesService.GetAll(competencyCategoryID);
            competency.Name = "";
            this.StateHasChanged();

        }




        protected async Task GetCompetency(int id)
        {
            competency = await CompetenciesService.Get(id);
            edit = true;

        }

        protected async Task GetCompetencyCategory(int id)
        {
            competencyCategory = await CompetenciesCategoryService.Get(id);
            edit = true;

        }

        protected async Task DeleteCategory(int id)
        {
            await CompetenciesCategoryService.Delete(id);

            await OnInitializedAsync();
            NavigationManager.NavigateTo($"/adminCompetency");
            competency = new Competency();


        }

        protected async Task DeleteCompetency(int id)
        {
            await CompetenciesService.Delete(id);

            competency.CatagoryId = competencyCategoryID;
            competencies = await CompetenciesService.GetAll(competencyCategoryID);
            competency.Name = "";
            this.StateHasChanged();


        }
    }
}