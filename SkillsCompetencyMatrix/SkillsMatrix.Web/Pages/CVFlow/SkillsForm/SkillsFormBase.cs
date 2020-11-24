using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SkillsMatrix.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Blazored.Toast.Services;
using SkillsMatrix.Web.Services.Shared;

namespace SkillsMatrix.Web.Pages.CVFlow.SkillsForm
{
    public class SkillsFormBase : ComponentBase
    {
        [Inject]
        public IPersonService PersonService { get; set; }
        [Inject]
        public IPercentageCalc PercentageCalcService { get; set; }
        public int percentage = 0;

        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public ISkillsService SkillsService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        public int UserId { get; set; }

        private bool edit = false;

        [Parameter]
        public string PersonId { get; set; }

        protected Skill skill = new Skill();

        public string SkillLevel = "0";

        protected List<Skill> skills = new List<Skill>();

        protected EditContext editContext;


        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            editContext = new EditContext(skill);

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;


                    skills = await SkillsService.GetSkills(user.Id);


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
                        skill.UserId = UserId;
                        skill.Level = int.Parse(SkillLevel);
                        await SkillsService.Create(skill);
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
                        await OnInitializedAsync();
                        skill = new Skill();
                        SkillLevel = "0";
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
                        skill.Level = int.Parse(SkillLevel);
                        await SkillsService.Update(skill);
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
                        await OnInitializedAsync();
                        edit = false;
                        skill = new Skill();
                        SkillLevel = "0";
                        this.StateHasChanged();
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


        protected void Back()
        {
            NavigationManager.NavigateTo($"/membership");
        }

        protected void Next()
        {
            NavigationManager.NavigateTo($"/extraactivities");
        }

        protected async void Cancel()
        {
            skill = new Skill();
            NavigationManager.NavigateTo($"/skills");

        }


        protected async Task GetSkill(int id)
        {
            skill = await SkillsService.Get(id);
            SkillLevel = skill.Level.ToString();
            await OnInitializedAsync();
            edit = true;

        }

        protected async Task DeleteSkill(int id)
        {
            await SkillsService.Delete(id);
            percentage = await PercentageCalcService.ProfileCompletion(UserId);
            await PersonService.UpdatePercentageComletion(UserId, percentage);
            await OnInitializedAsync();
            NavigationManager.NavigateTo($"/skills");
            skill = new Skill();
            toastService.ShowWarning("Skill is removed", "Warning");

        }
    }
}