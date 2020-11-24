using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using SkillsMatrix.Models;
using SkillsMatrix.Web.Services;
using SkillsMatrix.Web.Services.Shared;
using System;
using System.Linq;
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
        public IPercentageCalc PercentageCalcService { get; set; }
        public int percentage = 0;
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
            bool validID = CheckID(person.IdNumber);
            if (validID != true)
            {
                toastService.ShowError("Invalid ID Number", "Error");
            }

           else if (isValid)
            {
                //Check if its a new record 
                if (person.Id == 0)
                {
                    try
                    {
                        person.UserId = UserId;
                        await PersonService.Create(person);
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId,percentage);
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
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
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

           bool validID = CheckID(person.IdNumber);
            if(validID!= true)
            {
                toastService.ShowError("Invalid ID Number", "Error");
            }
            else if (isValid)
            {
                //Check if its a new record 
                if (person.Id == 0)
                {
                  
                    try
                    {
                        person.UserId = UserId;
                        await PersonService.Create(person);
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
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
                        percentage = await PercentageCalcService.ProfileCompletion(UserId);
                        await PersonService.UpdatePercentageComletion(UserId, percentage);
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

        public  void ValidateID(ChangeEventArgs e)
        {

            string IdentityNumber;
            bool IsSouthAfrican;
            bool IsValid;

            IdentityNumber = (Convert.ToString(e.Value) ?? string.Empty).Replace(" ", "");
            if (IdentityNumber.Length == 13)
            {
                var digits = new int[13];
                for (int i = 0; i < 13; i++)
                {
                    digits[i] = int.Parse(IdentityNumber.Substring(i, 1));
                }
                int control1 = digits.Where((v, i) => i % 2 == 0 && i < 12).Sum();
                string second = string.Empty;
                digits.Where((v, i) => i % 2 != 0 && i < 12).ToList().ForEach(v =>
                      second += v.ToString());
                var string2 = (int.Parse(second) * 2).ToString();
                int control2 = 0;
                for (int i = 0; i < string2.Length; i++)
                {
                    control2 += int.Parse(string2.Substring(i, 1));
                }
                var control = (10 - ((control1 + control2) % 10))% 10;
                if (digits[12] == control)
                {
                    person.DateOfBirth = DateTime.ParseExact(IdentityNumber
                        .Substring(0, 6), "yyMMdd", null);
                    person.Gender = digits[6] < 5 ? "Female" : "Male";
                    IsSouthAfrican = digits[10] == 0;
                    if (IsSouthAfrican)
                    {
                        person.Nationality = "South Africa";
                    }
                    IsValid = true;
                    if(IsValid != true)
                    {
                        toastService.ShowError("Invalid Id Number");
                    }

                }

              

            }
            else 
            {
                    toastService.ShowError("Invalid Id Number");
             
            }

            person.IdNumber = (Convert.ToInt64(IdentityNumber));
        }

        public bool CheckID(long idNumber)
        {

            string IdentityNumber;
            bool IsValid = false;

            IdentityNumber = (Convert.ToString(idNumber) ?? string.Empty).Replace(" ", "");
            if (IdentityNumber.Length == 13)
            {
                var digits = new int[13];
                for (int i = 0; i < 13; i++)
                {
                    digits[i] = int.Parse(IdentityNumber.Substring(i, 1));
                }
                int control1 = digits.Where((v, i) => i % 2 == 0 && i < 12).Sum();
                string second = string.Empty;
                digits.Where((v, i) => i % 2 != 0 && i < 12).ToList().ForEach(v =>
                      second += v.ToString());
                var string2 = (int.Parse(second) * 2).ToString();
                int control2 = 0;
                for (int i = 0; i < string2.Length; i++)
                {
                    control2 += int.Parse(string2.Substring(i, 1));
                }
                var control = (10 - ((control1 + control2) % 10)) % 10;
                if (digits[12] == control)
                {
                    IsValid = true;


                }




            }

            return IsValid;
        }

    }

}

