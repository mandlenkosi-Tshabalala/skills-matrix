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

        public async Task<UserExpertise> Create(UserExpertise PersonExpertise)
        {
            return await httpClient.PostJsonAsync<UserExpertise>($"api/UserExpertise", PersonExpertise);
        }

        public async Task<UserExpertise> Get(int Id)
        {
           return await httpClient.GetJsonAsync<UserExpertise>($"api/UserExpertise/{Id}");
        }

        public async Task<UserExpertise> Update(UserExpertise PersonExpertise)
        {
            return await httpClient.PutJsonAsync<UserExpertise>($"api/UserExpertise", PersonExpertise);
        }

        public async Task<IEnumerable<UserExpertise>> GetExpertiseCategories()
        {
            return await httpClient.GetJsonAsync<IEnumerable<UserExpertise>>($"api/UserExpertise/GetExpertiseCategories");
        }

        public async Task<List<UserExpertise>> GetAll(int UserID)
        {
            return await httpClient.GetJsonAsync<List<UserExpertise>>($"api/UserExpertise/List/{UserID}");
        }

        public async Task Delete(int Id)
        {
            await httpClient.DeleteAsync($"api/UserExpertise/{Id}");
        }
    }
}
