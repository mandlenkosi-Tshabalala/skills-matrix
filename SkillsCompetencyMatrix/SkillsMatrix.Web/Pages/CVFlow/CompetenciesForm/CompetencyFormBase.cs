using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.Toast.Services;

namespace SkillsMatrix.Web.Pages.CVFlow.CompetenciesForm
{
    public class CompetencyFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IPersonCompetencies personCompetencies { get; set; }

        [Inject]
        public ICompetenciesService competenciesService { get; set; }

        [Inject]
        public ICompetenciesCategoryService competenciesCategoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }

        protected List<UserCompetency> userCompetencies = new List<UserCompetency>();

        protected IEnumerable<Competency> competencies;
        protected IEnumerable<CompetencyCategory> competencyCategories;

        protected UserCompetency UserCompetency = new UserCompetency();

        protected EditContext editContext;

        protected string competencyID { get; set; }

        public string competencyCategoryID { get; set; }


        private bool edit = false;

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(UserCompetency);

            if (principalUser.Identity.IsAuthenticated)
            {



                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;
                    //competencies = await competenciesService.GetCompetencies();

                    userCompetencies = await personCompetencies.GetAll(user.Id);

                    competencyCategories = await competenciesCategoryService.GetCompetencies();


                }
            }

        }

        protected async void HandleValidSubmit()
        {


            if (edit == false)
            {
                UserCompetency.UserId = UserId;
                UserCompetency.CompetencyId = Int32.Parse(competencyID);
                await personCompetencies.Create(UserCompetency);
                await OnInitializedAsync();
                UserCompetency = new UserCompetency();
                this.StateHasChanged();

            }
            else
            {
                await personCompetencies.Update(UserCompetency);
                await OnInitializedAsync();
                edit = false;
                NavigationManager.NavigateTo($"/personCompetencies");
                UserCompetency = new UserCompetency();


            }

        }


        protected async Task Delete(int id)
        {

            if (id != 0)
            {
                await personCompetencies.Delete(UserId, id);

                await OnInitializedAsync();
                this.StateHasChanged();

            }


        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/expertise");
        }

        protected void Next()
        {
            NavigationManager.NavigateTo("/membership");
        }

        protected async void competencyCategoryClicked(object competencyCategory)
        {
            competencyCategoryID = competencyCategory.ToString();
            competencies = await competenciesService.GetAll(Convert.ToInt32(competencyCategoryID));
            this.StateHasChanged();
        }

        protected async void Cancel()
        {
            UserCompetency = new UserCompetency();
            NavigationManager.NavigateTo($"/personCompetencies");

        }


        protected async Task GetExpertise(int id)
        {
            UserCompetency = await personCompetencies.Get(id);
            edit = true;

        }

       
    }
}