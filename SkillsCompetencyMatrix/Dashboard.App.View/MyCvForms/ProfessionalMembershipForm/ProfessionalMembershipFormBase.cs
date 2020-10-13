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
        public IAddressService AddressService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected ProfessionalMembership professionalMembership = new ProfessionalMembership();

        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {

            editContext = new EditContext(professionalMembership);

            //address = await AddressService.Get(1);

            //if (address != null)
            //{
            //    editContext = new EditContext(address);
            //}

        }

        protected void HandleValidSubmit()
        {

            //Check if its a new record 
            //if (address.Id == 0)
            //{

                //AddressService.Create(address);
                NavigationManager.NavigateTo("/personDetails");
            //}
            //else
            //{
                // AddressService.Update(address);
                NavigationManager.NavigateTo("/personDetails");
            //}

        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/personDetails");
        }
    }
}
