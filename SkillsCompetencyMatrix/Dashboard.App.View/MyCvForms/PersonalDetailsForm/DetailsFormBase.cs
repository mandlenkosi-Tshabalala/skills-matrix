using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Extensions;
using Skclusive.Blazor.Dashboard.App.View.Services;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.MyCvForms
{
    public class DetailsFormBase : ComponentBase
    {

        [Inject]
        public IPersonService PersonService { get; set; }

        [Inject]

        public NavigationManager NavigationManager { get; set; }

        protected Person person = new Person();


        protected EditContext editContext;
      
        protected override async Task  OnInitializedAsync()
        {

            editContext = new EditContext(person);

            person = await PersonService.GetPerson(1);

            if (person != null)
            {
                editContext = new EditContext(person);
            }

        }

        protected void HandleValidSubmit()
        {    
            //Check if its a new record 
            if (person.Id == 0)
            {
               PersonService.Create(person);
                NavigationManager.NavigateTo("/address");
            }
            else
            {
               PersonService.Update(person);
                NavigationManager.NavigateTo("/address");
            }

        }

    }
}
