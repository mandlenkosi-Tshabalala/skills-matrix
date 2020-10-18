using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public class EducationService : IEducationService
    {

        public readonly HttpClient httpClient;

        public EducationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Education> Create(Education person)
        {
            return await httpClient.PostJsonAsync<Education>($"api/Persons", person);
        }

        public async Task<Education> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Education>($"api/Persons/{Id}");
        }

        public async Task<Education> Update(Education person)
        {
            return await httpClient.PutJsonAsync<Education>($"api/Persons", person);
        }
    }
}
