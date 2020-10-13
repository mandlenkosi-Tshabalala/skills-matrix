using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public class ProfessionalMembershipService : IProfessionalMembershipService
    {

        public readonly HttpClient httpClient;

        public ProfessionalMembershipService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ProfessionalMembership> Create(ProfessionalMembership person)
        {
            return await httpClient.PostJsonAsync<ProfessionalMembership>($"api/Persons", person);
        }

        public async Task<ProfessionalMembership> Get(int Id)
        {
           return await httpClient.GetJsonAsync<ProfessionalMembership>($"api/Persons/{Id}");
        }

        public async Task<ProfessionalMembership> Update(ProfessionalMembership person)
        {
            return await httpClient.PutJsonAsync<ProfessionalMembership>($"api/Persons", person);
        }
    }
}
