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
    public class EducationFormBase : ComponentBase
    {

        [Inject]
        public IEducationService EducationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected Education personEducation = new Education();

        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {

            editContext = new EditContext(personEducation);

            personEducation = await EducationService.Get(1);

            if (personEducation != null)
            {
                editContext = new EditContext(personEducation);
            }

        }

        protected void HandleValidSubmit()
        {

            //Check if its a new record 
            if (personEducation.Id == 0)
            {

                EducationService.Create(personEducation);
                NavigationManager.NavigateTo("/personEmpolyment");
            }
            else
            {
                EducationService.Update(personEducation);
                NavigationManager.NavigateTo("/personEmpolyment");
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/address");
        }
    }
}
