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

        public async Task<Membership> Create(Membership person)
        {
            return await httpClient.PostJsonAsync<Membership>($"api/Persons", person);
        }

        public async Task<Membership> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Membership>($"api/Persons/{Id}");
        }

        public async Task<Membership> Update(Membership person)
        {
            return await httpClient.PutJsonAsync<Membership>($"api/Persons", person);
        }
    }
}
