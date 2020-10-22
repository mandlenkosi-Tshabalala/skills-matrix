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

        public async Task<UserCompetency> Create(UserCompetency person)
        {
            return await httpClient.PostJsonAsync<UserCompetency>($"api/PersonCompetencies", person);
        }

        public async Task<UserCompetency> Get(int Id)
        {
           return await httpClient.GetJsonAsync<UserCompetency>($"api/PersonCompetencies/{Id}");
        }

        public async Task<UserCompetency> Update(UserCompetency person)
        {
            return await httpClient.PutJsonAsync<UserCompetency>($"api/PersonCompetencies", person);
        }

        public async Task<IEnumerable<UserCompetency>> GetCompetencies()
        {
            return await httpClient.GetJsonAsync<IEnumerable<UserCompetency>>($"api/PersonCompetencies/GetCompetencies");
        }

        public async Task<List<UserCompetency>> GetAll(int UserID)
        {
            return await httpClient.GetJsonAsync<List<UserCompetency>>($"api/PersonCompetencies/List/{UserID}");
        }



        public async Task Delete(int Id)
        {
            //return await httpClient.DeleteAsync<Education>($"api/Educations/Delete", Id.ToString());
            await httpClient.DeleteAsync($"api/Educations/{Id}");
        }

    }
}
