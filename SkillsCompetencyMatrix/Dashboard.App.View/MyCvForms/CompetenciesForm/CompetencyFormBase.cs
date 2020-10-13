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
    public class CompetencyFormBase : ComponentBase
    {

        [Inject]
        public IAddressService AddressService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected Competency personCompetencies = new Competency();

        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {

            editContext = new EditContext(personCompetencies);

            //address = await AddressService.Get(1);

            //if (address != null)
            //{
            //    editContext = new EditContext(address);
            //}

        }

        protected void HandleValidSubmit()
        {

            //Check if its a new record 
            if (personCompetencies.Id == 0)
            {
                NavigationManager.NavigateTo("/viewCV");
                //AddressService.Create(personCompetencies);
            }
            else
            {
                NavigationManager.NavigateTo("/personDetails");
                // AddressService.Update(personCompetencies);
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/expertise");
        }
    }
}
