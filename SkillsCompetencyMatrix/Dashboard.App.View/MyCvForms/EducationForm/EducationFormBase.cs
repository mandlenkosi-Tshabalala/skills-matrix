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

        [Parameter]
        public string UserId { get; set; }

        protected Education personEducation = new Education();

        protected EditContext editContext;

        protected override async Task OnInitializedAsync()
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
            //if (personEducation != null)
            //    personEducation.PersonId = int.Parse(UserId);

            //Check if its a new record 
            if (personEducation.Id == 0)
            {

                EducationService.Create(personEducation);
                NavigationManager.NavigateTo($"/personEmpolyment/{UserId}");
            }
            else
            {
                EducationService.Update(personEducation);
                NavigationManager.NavigateTo($"/personEmpolyment/{UserId}");
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/address/{UserId}");
        }
    }
}
