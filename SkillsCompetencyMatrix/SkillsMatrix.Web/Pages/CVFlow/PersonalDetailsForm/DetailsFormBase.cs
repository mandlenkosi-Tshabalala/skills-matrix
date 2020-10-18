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

namespace SkillsMatrix.Web.Pages.CVFlow.PersonalDetailsForm
{
    public class DetailsFormBase : ComponentBase
    {

        [Inject]
        public IPersonService PersonService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserId { get; set; }

        protected PersonalInfo person = new PersonalInfo();


        protected EditContext editContext;

        protected override async Task OnInitializedAsync()
        {
            UserId = UserId ?? "8";


            editContext = new EditContext(person);

            person = await PersonService.GetPerson(int.Parse(UserId));

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
                person.UserId = 9;
                PersonService.Create(person);
                NavigationManager.NavigateTo($"/address/{person.Id}");
            }
            else
            {
                PersonService.Update(person);
                NavigationManager.NavigateTo($"/address/{person.Id}");
            }
        }
    }
}
