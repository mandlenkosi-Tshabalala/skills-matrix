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
    public class ExpertiseService : IExpertiseService
    {

        public readonly HttpClient httpClient;

        public ExpertiseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Expertise> Create(Expertise Expertise)
        {
            return await httpClient.PostJsonAsync<Expertise>($"api/Expertises", Expertise);
        }

        public async Task<Expertise> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Expertise>($"api/Expertises/{Id}");
        }

        public async Task<Expertise> Update(Expertise Expertise)
        {
            return await httpClient.PutJsonAsync<Expertise>($"api/Expertises", Expertise);
        }


        public async Task<List<Expertise>> GetAll()
        {
            return await httpClient.GetJsonAsync<List<Expertise>>($"api/Expertises");
        }

        public async Task Delete(int Id)
        {
            await httpClient.DeleteAsync($"api/Expertises/{Id}");
        }
    }
}
