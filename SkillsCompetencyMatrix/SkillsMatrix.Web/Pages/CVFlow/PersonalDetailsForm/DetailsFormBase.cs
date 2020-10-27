﻿using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Models;
using SkillsMatrix.Web.Services;
using System;

using System.Threading.Tasks;

namespace SkillsMatrix.Web.Pages.CVFlow.PersonalDetailsForm
{
    public class DetailsFormBase : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; } 
        [Inject]
        public IPersonService PersonService { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public int UserId { get; set; }

        protected PersonalInfo person = new PersonalInfo();


        protected EditContext editContext;

        protected override async Task OnInitializedAsync()
        {
            var principalUser = (await AuthState).User;

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;
                    editContext = new EditContext(person);

                    person = await PersonService.GetPersonByUserId(user.Id);

                    if (person != null)
                    {
                        editContext = new EditContext(person);
                    }
                }
            }        
        }

        protected async void HandleValidSubmit()
        {
            var isValid = editContext.Validate();

            if (isValid)
            {
                //Check if its a new record 
                if (person.Id == 0)
                {
                    try
                    {
                        person.UserId = UserId;
                        await PersonService.Create(person);
                        NavigationManager.NavigateTo($"/address");
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
                        await PersonService.Update(person);
                        NavigationManager.NavigateTo($"/address");
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

        protected async void QuickSave()
        {
            var isValid = editContext.Validate();

            if (isValid)
            {
                //Check if its a new record 
                if (person.Id == 0)
                {
                  
                    try
                    {
                        person.UserId = UserId;
                        await PersonService.Create(person);                   
                        toastService.ShowSuccess("The information has been saved successfully", "Saved");
                        await OnInitializedAsync();
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
                        await PersonService.Update(person);
                        toastService.ShowSuccess("The information has been saved successfully", "Success");
                        await OnInitializedAsync();
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
    }
}
