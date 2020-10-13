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
        public IAddressService AddressService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected SkillsMatrix.Models.Education personEducation = new SkillsMatrix.Models.Education();

        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {

            editContext = new EditContext(personEducation);

            //address = await AddressService.Get(1);

            //if (address != null)
            //{
            //    editContext = new EditContext(address);
            //}

        }

        protected void HandleValidSubmit()
        {

            //Check if its a new record 
            if (personEducation.Id == 0)
            {

                //AddressService.Create(personEducation);
                NavigationManager.NavigateTo("/personEmpolyment");
            }
            else
            {
                //AddressService.Update(personEducation);
                NavigationManager.NavigateTo("/personEmpolyment");
            }

        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/address");
        }
    }
}
