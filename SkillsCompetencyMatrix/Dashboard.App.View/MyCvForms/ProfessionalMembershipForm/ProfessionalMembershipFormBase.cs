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

        protected Membership membership = new Membership();

        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {
            // fetch all from DB and assign
            editContext = new EditContext(membership);

            membership = await ProfessionalMembershipService.Get(1);

            if (membership != null)
            {
                editContext = new EditContext(membership);
            }

        }

        protected void HandleValidSubmit()
        {
            //Check if its a new record
            if (membership.Id == 0)
            {

                ProfessionalMembershipService.Create(membership);
                NavigationManager.NavigateTo($"/personDetails/{PersonId}");
            }
            else
            {
                ProfessionalMembershipService.Update(membership);
                NavigationManager.NavigateTo($"/personDetails/{PersonId}");
            }
        }

        protected void Back()
        {
            NavigationManager.NavigateTo($"/personDetails/{PersonId}");
        }
    }
}
