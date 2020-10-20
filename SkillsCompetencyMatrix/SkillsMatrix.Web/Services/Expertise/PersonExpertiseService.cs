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
    public class PersonExpertiseService : IPersonExpertiseService
    {

        public readonly HttpClient httpClient;

        public PersonExpertiseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Expertise> Create(Expertise person)
        {
            return await httpClient.PostJsonAsync<Expertise>($"api/Persons", person);
        }

        public async Task<Expertise> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Expertise>($"api/Persons/{Id}");
        }

        public async Task<Expertise> Update(Expertise person)
        {
            return await httpClient.PutJsonAsync<Expertise>($"api/Persons", person);
        }

        public async Task<IEnumerable<Expertise>> GetExpertiseCategories()
        {
            return await httpClient.GetJsonAsync<IEnumerable<Expertise>>($"api/Expertises/GetExpertiseCategories");
        }
    }
}
