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
using SkillsMatrix.Web.Services.Shared;

namespace SkillsMatrix.Web.Pages.CVFlow.CompetenciesForm
{
    public class CompetencyFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IPersonCompetencies personCompetencies { get; set; }

        [Inject]
        public IPersonService PersonService { get; set; }
        [Inject]
        public IPercentageCalc PercentageCalcService { get; set; }
        public int percentage = 0;
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
        public List<string> subCompetencyIDs = new List<string>();
        public Competency _competency;

        public Dictionary<int, bool> expandCompetencies = new Dictionary<int, bool>();
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
                    competencies = null;
                    competencyCategories = null;
                    userCompetencies = await personCompetencies.GetAll(user.Id);

                    expandCompetencies.Clear();
                    foreach (var userCompetency in userCompetencies)
                    {
                        expandCompetencies.Add(userCompetency.CompetencyId, false);
                    }

                    competencyCategories = await competenciesCategoryService.GetCompetencies();


                }
            }

        }

        protected async void HandleValidSubmit()
        {
            var isValid = editContext.Validate();

            if (isValid)
            {
                

                    if (edit == false)
                    {
                    try
                    {
                        UserCompetency.UserId = UserId;
                        UserCompetency.CompetencyId = Int32.Parse(competencyID);
                        UserCompetency.SubCompetencyIds = new List<int>();
                        foreach (var subCompetencyID in subCompetencyIDs)
                        {
                            UserCompetency.SubCompetencyIds.Add(Int32.Parse(subCompetencyID));
                        }
                        await personCompetencies.Create(UserCompetency);
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
                        await OnInitializedAsync();
                        UserCompetency = new UserCompetency();
                        this.StateHasChanged();
                        toastService.ShowSuccess("The information has been saved successfully", "Saved");
                    }
                    catch (Exception ex)
                {
                    toastService.ShowError("There was an error when trying to save", "Error");
                }
            }
            else
            {
                try
                {
                    await personCompetencies.Update(UserCompetency);
                    percentage = await PercentageCalcService.ProfileCompletion(UserId);
                    await PersonService.UpdatePercentageComletion(UserId, percentage);
                    await OnInitializedAsync();
                    edit = false;
                    NavigationManager.NavigateTo($"/personCompetencies");
                    UserCompetency = new UserCompetency();
                    toastService.ShowSuccess("The information has been saved successfully", "Saved");
                }
                catch (Exception ex)
                {
                    toastService.ShowError("There was an error when trying to save", "Error");
                }

            }
        }
    
            else
            {
                toastService.ShowError("Please make sure that you fill all required field", "Error");

            }

        }


        protected async Task Delete(int id)
        {

            if (id != 0)
            {
                await personCompetencies.Delete(UserId, id);
                percentage = await PercentageCalcService.ProfileCompletion(UserId);
                await PersonService.UpdatePercentageComletion(UserId, percentage);
                await OnInitializedAsync();
                this.StateHasChanged();
                toastService.ShowWarning("Competency is removed", "Warning");
            }


        }

        protected void ExpandUserCompetency(int id)
        {

            if (id != 0)
            {
                if (expandCompetencies[id] == false)
                {
                    expandCompetencies[id] = true;
                }

                else
                {
                    expandCompetencies[id] = false;
                }
                
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

        protected async void CompetencyCategoryClicked(object competencyCategory)
        {
            competencyCategoryID = competencyCategory.ToString();
            competencies = await competenciesService.GetAll(Convert.ToInt32(competencyCategoryID));
            subCompetencyIDs.Clear();
            this.StateHasChanged();
        }

        protected void CompetencyClicked(object id)
        {
            competencyID = id.ToString();
            foreach (var __competency in competencies)
            {
                if(__competency.Id == Int32.Parse(id.ToString()))
                {
                    _competency = __competency;
                    break;
                }
            }
            
            subCompetencyIDs.Clear();
            this.StateHasChanged();
        }

        protected void SubCompetencyClicked(SubCompetency SubCompetency, object selected)
        {
            if ((bool)selected)
            {
                subCompetencyIDs.Add(SubCompetency.Id.ToString());
            }

            else
            {
                subCompetencyIDs.Remove(SubCompetency.Id.ToString());
            }
            
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