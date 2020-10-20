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

namespace SkillsMatrix.Web.Pages.CVFlow.EmploymentHistoryForm
{
    public class EmployementFormBase : ComponentBase
    {

        [Inject]
        public IEmployementHistoryService EmployementService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserId { get; set; }

        protected Employment EmploymentHistory = new Employment();

        protected EditContext editContext;

        protected override async Task OnInitializedAsync()
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
            //if (EmploymentHistory != null)
            //    EmploymentHistory.UserId = int.Parse(UserId);

            //Check if its a new record 
            if (EmploymentHistory.Id == 0)
            {
                EmployementService.Create(EmploymentHistory);
                NavigationManager.NavigateTo($"/expertise/{UserId}");
            }
            else
            {
                EmployementService.Update(EmploymentHistory);
                NavigationManager.NavigateTo($"/expertise/{UserId}");
            }
        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/personEducation/{UserId}");
        }
    }
}
