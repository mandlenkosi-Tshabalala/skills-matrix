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

        public async Task<Employment> Create(Employment EmploymentHistory)
        {
            return await httpClient.PostJsonAsync<Employment>($"api/Persons", EmploymentHistory);
        }

        public async Task<Employment> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Employment>($"api/Persons/{Id}");
        }

        public async Task<Employment> Update(Employment EmploymentHistory)
        {
            return await httpClient.PutJsonAsync<Employment>($"api/Persons", EmploymentHistory);
        }
    }
}
