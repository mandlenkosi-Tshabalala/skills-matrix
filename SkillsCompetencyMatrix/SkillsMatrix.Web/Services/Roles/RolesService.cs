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
    public class RolesService : IRolesService
    {

        public readonly HttpClient httpClient;

        public RolesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Role> Create(Role role)
        {
            return await httpClient.PostJsonAsync<Role>($"api/Roles", role);
        }

        public async Task<Role> Get(int Id)
        {
            return await httpClient.GetJsonAsync<Role>($"api/Roles/{Id}");
        }

        public async Task<Role> Update(Role role)
        {
            return await httpClient.PutJsonAsync<Role>($"api/Roles", role);
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await httpClient.GetJsonAsync<IEnumerable<Role>>($"api/Roles/GetRoles");
        }

        public async Task<List<Role>> GetAll()
        {
            return await httpClient.GetJsonAsync<List<Role>>($"api/Roles");
        }



        public async Task Delete(int Id)
        {
            //return await httpClient.DeleteAsync<Education>($"api/Educations/Delete", Id.ToString());
            await httpClient.DeleteAsync($"api/Roles/{Id}");
        }

    }
}
