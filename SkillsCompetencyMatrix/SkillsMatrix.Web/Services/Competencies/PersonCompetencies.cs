using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public class PersonCompetencies : IPersonCompetencies
    {

        public readonly HttpClient httpClient;

        public PersonCompetencies(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Competency> Create(Competency person)
        {
            return await httpClient.PostJsonAsync<Competency>($"api/Persons", person);
        }

        public async Task<Competency> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Competency>($"api/Persons/{Id}");
        }

        public async Task<Competency> Update(Competency person)
        {
            return await httpClient.PutJsonAsync<Competency>($"api/Persons", person);
        }

        public async Task<IEnumerable<Competency>> GetCompetencies()
        {
            return await httpClient.GetJsonAsync<IEnumerable<Competency>>($"api/Competencies/GetCompetencies");
        }
    }
}
