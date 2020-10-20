using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SkillsMatrix.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Pages.CVFlow.CompetenciesForm
{
    public class CompetencyFormBase : ComponentBase
    {

        [Inject]
        public IPersonCompetencies PersonCompetencies { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string PersonId { get; set; }

        protected Competency personCompetencies = new Competency();

        protected EditContext editContext;

        protected override async Task OnInitializedAsync()
        {

            editContext = new EditContext(personCompetencies);

            personCompetencies = await PersonCompetencies.Get(1);

            if (personCompetencies != null)
            {
                editContext = new EditContext(personCompetencies);
            }

        }

        protected void HandleValidSubmit()
        {
            //if (personCompetencies != null)
            //    personCompetencies.PersonID = int.Parse(PersonId);

            //Check if its a new record 
            if (personCompetencies.Id == 0)
            {
                PersonCompetencies.Create(personCompetencies);
                NavigationManager.NavigateTo($"/viewCV/{PersonId}");
            }
            else
            {
                PersonCompetencies.Update(personCompetencies);
                NavigationManager.NavigateTo($"/personDetails/{PersonId}");
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/expertise/{PersonId}");
        }
    }
}
