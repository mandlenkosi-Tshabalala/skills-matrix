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
    public class ProfessionalMembershipService : IProfessionalMembershipService
    {

        public readonly HttpClient httpClient;

        public ProfessionalMembershipService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Membership> Create(Membership person)
        {
            return await httpClient.PostJsonAsync<Membership>($"api/ProfessionalMemberships", person);
        }

        public async Task<Membership> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Membership>($"api/ProfessionalMemberships/{Id}");
        }

        public async Task<Membership> Update(Membership person)
        {
            return await httpClient.PutJsonAsync<Membership>($"api/ProfessionalMemberships", person);
        }

        public async Task<List<Membership>> GetAll(int UserID)
        {
            return await httpClient.GetJsonAsync<List<Membership>>($"api/ProfessionalMemberships/List/{UserID}");
        }

        public async Task Delete(int Id)
        {
            //return await httpClient.DeleteAsync<Education>($"api/Educations/Delete", Id.ToString());
            await httpClient.DeleteAsync($"api/ProfessionalMemberships/{Id}");
        }
    }
}
