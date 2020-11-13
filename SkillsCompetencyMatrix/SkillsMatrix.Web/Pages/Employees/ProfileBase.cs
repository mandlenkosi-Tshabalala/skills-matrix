using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Models;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Forms;


namespace SkillsMatrix.Web.Pages.Employees
{
    public class ProfileBase : ComponentBase
    {
        [Inject]
        public IFileUploadService uploadService {get;set;}
        protected IFileListEntry file;
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public IPersonService PersonService { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        public bool Searching = true;

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        protected EditContext editContext;


        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public int UserId { get; set; }
        public int PersonId { get; set; }

        protected PersonalInfo Person = new PersonalInfo();

        protected override async Task OnInitializedAsync()
        {
              Searching = true;

           var principalUser = (await AuthState).User;

            editContext = new EditContext(Person);

            if (principalUser.Identity.IsAuthenticated)
            {
                var user = await UserManager.FindByEmailAsync(principalUser.Identity.Name);
                if (user != null)
                {
                    UserId = user.Id;
                    Person = await PersonService.GetPersonByUserId(user.Id);

                    editContext = new EditContext(Person);

                    if (Person != null)
                    {
                        PersonId = Person.Id;
                    }

                    Searching = false;
                }
            }
        }

        public async Task HandleFileSelected(IFileListEntry[] files)
        {
            file = files.FirstOrDefault();
            if(file != null)
            {
                var path = await uploadService.UploadAsync(file);
                if (!string.IsNullOrEmpty(path))
                {
                    Person.ImagePath = file.Name;
                    await PersonService.Update(Person);
                }
            }

        }

        protected async void QuickSave()
        {
            var isValid = editContext.Validate();

            if (isValid)
            {

                    try
                    {
                        await PersonService.Update(Person);
                        toastService.ShowSuccess("The information has been saved successfully", "Success");
                        await OnInitializedAsync();
                    }
                    catch (Exception ex)
                    {
                        toastService.ShowError("There was an error when trying to save", "Error");
                    }

                


            }
            else
            {
                toastService.ShowError("Please make sure that you fill all required field", "Error");
            }
        }
    }
}
