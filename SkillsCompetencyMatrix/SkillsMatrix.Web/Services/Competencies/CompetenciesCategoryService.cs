using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public class CompetenciesCategoryService : ICompetenciesCategoryService
    {

        public readonly HttpClient httpClient;

        public CompetenciesCategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CompetencyCategory> Create(CompetencyCategory person)
        {
            return await httpClient.PostJsonAsync<CompetencyCategory>($"api/CompetencyCategories", person);
        }

        public async Task<CompetencyCategory> Get(int Id)
        {
           return await httpClient.GetJsonAsync<CompetencyCategory>($"api/CompetencyCategories/{Id}");
        }

        public async Task<CompetencyCategory> Update(CompetencyCategory person)
        {
            return await httpClient.PutJsonAsync<CompetencyCategory>($"api/CompetencyCategories", person);
        }

        public async Task<IEnumerable<CompetencyCategory>> GetCompetencies()
        {
            return await httpClient.GetJsonAsync<IEnumerable<CompetencyCategory>>($"api/CompetencyCategories");
        }

        public async Task<List<CompetencyCategory>> GetAll()
        {
            return await httpClient.GetJsonAsync<List<CompetencyCategory>>($"api/CompetencyCategories");
        }



        public async Task Delete(int Id)
        {
            //return await httpClient.DeleteAsync<Education>($"api/Educations/Delete", Id.ToString());
            await httpClient.DeleteAsync($"api/CompetencyCategories/{Id}");
        }

    }
}
