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
    public class ExpertiseFormBase : ComponentBase
    {

        [Inject]
        public IPersonExpertiseService PersonExpertiseService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string PersonId { get; set; }

        protected Expertise personExpertise = new Expertise();

        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {

           editContext = new EditContext(personExpertise);

            personExpertise = await PersonExpertiseService.Get(1);

            if (personExpertise != null)
            {
                editContext = new EditContext(personExpertise);
            }

        }

        protected void HandleValidSubmit()
        {
            //if(personExpertise != null)
            //    personExpertise.PersonId

            //Check if its a new record 
            if (personExpertise.Id == 0)
            {

                PersonExpertiseService.Create(personExpertise);
                NavigationManager.NavigateTo($"/personCompetencies/{PersonId}");
            }
            else
            {
                PersonExpertiseService.Update(personExpertise);
                NavigationManager.NavigateTo($"/personCompetencies/{PersonId}");
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/personEmpolyment/{PersonId}");
        }
    }
}
