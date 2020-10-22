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
    public class SkillsService : ISkillsService
    {

        public readonly HttpClient httpClient;

        public SkillsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Skill> Create(Skill person)
        {
            return await httpClient.PostJsonAsync<Skill>($"api/UserSkills", person);
        }

        public async Task<Skill> Get(int Id)
        {
            return await httpClient.GetJsonAsync<Skill>($"api/UserSkills/{Id}");
        }

        public async Task<Skill> Update(Skill person)
        {
            return await httpClient.PutJsonAsync<Skill>($"api/UserSkills", person);
        }

        public async Task<List<Skill>> GetSkills(int UserID)
        {
            return await httpClient.GetJsonAsync<List<Skill>>($"api/UserSkills/List/{UserID}");
        }



        public async Task Delete(int Id)
        {
            await httpClient.DeleteAsync($"api/UserSkills/{Id}");
        }
    }
}
