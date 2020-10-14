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
    public class ProfessionalMembershipFormBase : ComponentBase
    {

        [Inject]
        public IProfessionalMembershipService ProfessionalMembershipService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string PersonId { get; set; }

        protected ProfessionalMembership professionalMembership = new ProfessionalMembership();

        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {

            editContext = new EditContext(professionalMembership);

            professionalMembership = await ProfessionalMembershipService.Get(1);

            if (professionalMembership != null)
            {
                editContext = new EditContext(professionalMembership);
            }

        }

        protected void HandleValidSubmit()
        {
            if (professionalMembership != null)
                professionalMembership.PersonId = int.Parse(PersonId);

            //Check if its a new record
            if (professionalMembership.Id == 0)
            {

                ProfessionalMembershipService.Create(professionalMembership);
                NavigationManager.NavigateTo($"/personDetails/{PersonId}");
            }
            else
            {
                ProfessionalMembershipService.Update(professionalMembership);
                NavigationManager.NavigateTo($"/personDetails/{PersonId}");
            }
        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/personDetails/{PersonId}");
        }
    }
}
