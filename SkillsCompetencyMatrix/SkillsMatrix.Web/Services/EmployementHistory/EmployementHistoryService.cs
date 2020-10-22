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
    public class EmployementHistoryService : IEmployementHistoryService
    {

        public readonly HttpClient httpClient;

        public EmployementHistoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Employment> Create(Employment EmploymentHistory)
        {
            return await httpClient.PostJsonAsync<Employment>($"api/EmploymentHistories", EmploymentHistory);
        }

        public async Task<Employment> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Employment>($"api/EmploymentHistories/{Id}");
        }

        public async Task<Employment> Update(Employment EmploymentHistory)
        {
            return await httpClient.PutJsonAsync<Employment>($"api/EmploymentHistories", EmploymentHistory);
        }

        public async Task Delete(int Id)
        {
            //return await httpClient.DeleteAsync<Education>($"api/Educations/Delete", Id.ToString());
            await httpClient.DeleteAsync($"api/EmploymentHistories/{Id}");
        }

        public async Task<List<Employment>> GetEmployment(int UserID)
        {
            return await httpClient.GetJsonAsync<List<Employment>>($"api/EmploymentHistories/List/{UserID}");
        }
    }
}
