using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public class CompetenciesService : ICompetenciesService
    {

        public readonly HttpClient httpClient;

        public CompetenciesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Competency> Create(Competency person)
        {
            return await httpClient.PostJsonAsync<Competency>($"api/Competencies", person);
        }

        public async Task<Competency> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Competency>($"api/Competencies/{Id}");
        }

        public async Task<Competency> Update(Competency person)
        {
            return await httpClient.PutJsonAsync<Competency>($"api/Competencies", person);
        }

        public async Task<IEnumerable<Competency>> GetCompetencies()
        {
            return await httpClient.GetJsonAsync<IEnumerable<Competency>>($"api/Competencies/GetCompetencies");
        }

        public async Task<List<Competency>> GetAll(int id)
        {
            return await httpClient.GetJsonAsync<List<Competency>>($"api/Competencies/List/{id}");
        }



        public async Task Delete(int Id)
        {
            //return await httpClient.DeleteAsync<Education>($"api/Educations/Delete", Id.ToString());
            await httpClient.DeleteAsync($"api/Educations/{Id}");
        }

    }
}
