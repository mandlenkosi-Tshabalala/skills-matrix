using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Skclusive.Blazor.Dashboard.App.View.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.MyCvForms
{
    public class EmployementFormBase : ComponentBase
    {

        [Inject]
        public IEmployementHistoryService EmployementService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected EmploymentHistory EmploymentHistory = new EmploymentHistory();

        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {

            editContext = new EditContext(EmploymentHistory);

            EmploymentHistory = await EmployementService.Get(1);

            if (EmploymentHistory != null)
            {
                editContext = new EditContext(EmploymentHistory);
            }

        }

        protected void HandleValidSubmit()
        {

            //Check if its a new record 
            if (EmploymentHistory.Id == 0)
            {

                EmployementService.Create(EmploymentHistory);
                NavigationManager.NavigateTo("/expertise");
            }
            else
            {
                EmployementService.Update(EmploymentHistory);
                NavigationManager.NavigateTo("/expertise");
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/personEducation");
        }
    }
}
