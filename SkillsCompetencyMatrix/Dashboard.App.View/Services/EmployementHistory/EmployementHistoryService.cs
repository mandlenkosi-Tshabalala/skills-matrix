using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public class EmployementHistoryService : IEmployementHistoryService
    {

        public readonly HttpClient httpClient;

        public EmployementHistoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<EmploymentHistory> Create(EmploymentHistory EmploymentHistory)
        {
            return await httpClient.PostJsonAsync<EmploymentHistory>($"api/Persons", EmploymentHistory);
        }

        public async Task<EmploymentHistory> Get(int Id)
        {
           return await httpClient.GetJsonAsync<EmploymentHistory>($"api/Persons/{Id}");
        }

        public async Task<EmploymentHistory> Update(EmploymentHistory EmploymentHistory)
        {
            return await httpClient.PutJsonAsync<EmploymentHistory>($"api/Persons", EmploymentHistory);
        }
    }
}
