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
        public IActivityService ActivityService { get; set; }


        [Inject]
        public ISkillsService SkillsService { get; set; }
        [Inject]
        public IPersonService personService { get; set; }

        [Inject]
        public IPersonCompetencies PersonCompetencies { get; set; }

        [Inject]
        public IPersonExpertiseService PersonExpertiseService { get; set; }

        [Inject]
        public IAddressService AddressService { get; set; }
        [Inject]
        public IEducationService educationService { get; set; }

        [Inject]
        public IEmployementHistoryService employementHistoryService { get; set; }

        [Inject]
        protected SignInManager<IdentityUser<int>> SignInManager { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> UserManager { get; set; }

        public bool Searching = true;

        public int Percentage;

        [CascadingParameter]
        protected Task<AuthenticationState> AuthState { get; set; }

        protected EditContext editContext;




        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public int UserId { get; set; }
        public int PersonId { get; set; }
        protected PersonalInfo Person = new PersonalInfo();
        protected Address address = new Address();
        protected List<Skill> skills = new List<Skill>();
        protected List<Education> educations = new List<Education>();
        protected List<Employment> employments = new List<Employment>();
        protected List<UserActivities> activities = new List<UserActivities>();
        protected List<UserCompetency> competencies = new List<UserCompetency>();
        protected List<UserExpertise> expertises = new List<UserExpertise>();
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
                    Percentage = await ProfileCompletion(UserId);
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

        public async Task <int> ProfileCompletion(int UserId)
        {
            int Total = 0;
            int t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0;

            Person = await PersonService.GetPersonByUserId(UserId);
            address = await AddressService.Get(UserId);
            skills = await SkillsService.GetSkills(UserId);
            educations = await educationService.GetEducations(UserId);
            employments = await employementHistoryService.GetEmployment(UserId);
            activities = await ActivityService.GetActivity(UserId);
            expertises = await PersonExpertiseService.GetAll(UserId);
            competencies = await PersonCompetencies.GetAll(UserId);

            if(Person != null)
            {
                t1 = 20;
            }
            if (address != null)
            {
                t2 = 20;
            }
            if (educations.Count != 0)
            {
                t3 = 10;
            }
            if (employments.Count != 0)
            {
                t4 = 10;
            }
            if (activities.Count != 0)
            {
                t5 = 10;
            }
            if (expertises.Count != 0)
            {
                t6 = 10;
            }
            if (competencies.Count != 0)
            {
                t7 = 10;
            }
            if (skills.Count != 0)
            {
                t8 = 10;
            }

            Total = t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8;

            return Total;
        }
    }
}
