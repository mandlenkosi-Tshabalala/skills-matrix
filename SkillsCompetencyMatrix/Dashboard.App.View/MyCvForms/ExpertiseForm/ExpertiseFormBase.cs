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
        public IAddressService AddressService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected Expertise personExpertise = new Expertise();

        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {

           editContext = new EditContext(personExpertise);

            //address = await AddressService.Get(1);

            //if (address != null)
            //{
            //    editContext = new EditContext(address);
            //}

        }

        protected void HandleValidSubmit()
        {

            //Check if its a new record 
            if (personExpertise.Id == 0)
            {

                // AddressService.Create(personExpertise);
                NavigationManager.NavigateTo("/personCompetencies");
            }
            else
            {
                // AddressService.Update(personExpertise);
                NavigationManager.NavigateTo("/personCompetencies");
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/personEmpolyment");
        }
    }
}
