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

namespace SkillsMatrix.Web.Pages.CVFlow.SkillsForm
{
    public class SkillsFormBase : ComponentBase
    {
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
                await SkillsService.Create(skill);
                await OnInitializedAsync();
                NavigationManager.NavigateTo($"/skills");
                skill = new Skill();
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
                await SkillsService.Update(skill);
                await OnInitializedAsync();
                edit = false;
                NavigationManager.NavigateTo($"/skills");
                skill = new Skill();
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
            NavigationManager.NavigateTo("/ViewCV");
        }

        protected async void Cancel()
        {
            skill = new Skill();
            NavigationManager.NavigateTo($"/skills");

        }


        protected async Task GetSkill(int id)
        {
            skill = await SkillsService.Get(id);
            edit = true;

        }

        protected async Task DeleteSkill(int id)
        {
            await SkillsService.Delete(id);

            await OnInitializedAsync();
            NavigationManager.NavigateTo($"/skills");
            skill = new Skill();
            toastService.ShowWarning("Skill is removed", "Warning");

        }
    }
}