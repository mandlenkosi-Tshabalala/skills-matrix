
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
            var isValid = editContext.Validate();

            if (isValid)
            {

                if (edit == false)
            {
               try
                {
                await CompetenciesCategoryService.Create(competencyCategory);
                await OnInitializedAsync();
                NavigationManager.NavigateTo($"/adminCompetency");
                competencyCategory = new CompetencyCategory();
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
                await CompetenciesCategoryService.Update(competencyCategory);
                await OnInitializedAsync();
                edit = false;
                NavigationManager.NavigateTo($"/adminCompetency");
                competencyCategory = new CompetencyCategory();
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

        protected async void HandleCompetency()
        {
            var isValid = editContext.Validate();

            if (isValid)
            {
                if (edit == false)
                {
                    try
                    {
                        competency.CatagoryId = competencyCategoryID;
                        await CompetenciesService.Create(competency);
                        await CompetenciesService.GetAll(competencyCategoryID);
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
                        await CompetenciesService.Update(competency);
                        await OnInitializedAsync();
                        edit = false;
                        NavigationManager.NavigateTo($"/adminCompetency");
                        competency = new Competency();
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

        protected async void SearchCompetency(ChangeEventArgs e)
        {
             competencyCategoryID = (Convert.ToInt32(e.Value));
             competencies = await  CompetenciesService.GetAll(competencyCategoryID);
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
            toastService.ShowWarning("Category is removed", "Warning");

        }

        protected async Task DeleteCompetency(int id)
        {
            await CompetenciesService.Delete(id);

            await OnInitializedAsync();
            NavigationManager.NavigateTo($"/adminCompetency");
            competencyCategory = new CompetencyCategory();
            toastService.ShowWarning("Competency is removed", "Warning");


        }
    }
}