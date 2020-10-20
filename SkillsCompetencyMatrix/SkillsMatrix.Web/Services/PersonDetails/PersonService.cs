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
    public class PersonService : IPersonService
    {

        public readonly HttpClient httpClient;

        public PersonService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PersonalInfo> Create(PersonalInfo person)
        {
            return await httpClient.PostJsonAsync<PersonalInfo>($"api/Persons", person);
        }

        public async Task<PersonalInfo> GetPerson(int Id)
        {
           return await httpClient.GetJsonAsync<PersonalInfo>($"api/Persons/{Id}");
        }

        public async Task<PersonalInfo> Update(PersonalInfo person)
        {
            return await httpClient.PutJsonAsync<PersonalInfo>($"api/Persons", person);
        }
        public async Task<IEnumerable<PersonalInfo>> GetAllEmployees()
        {
            return await httpClient.GetJsonAsync<IEnumerable<PersonalInfo>>($"api/Persons/GetEmployees");
        }


        public async Task<PersonalInfo> GetPersonByUserId(int Id)
        {
            return await httpClient.GetJsonAsync<PersonalInfo>($"api/Persons/GetByUserId/{Id}");
        }

        public async Task<IEnumerable<PersonalInfo>> GetAllEmployees(string EmployeeName, int expertiseID, int competencyID)
        {
            return await httpClient.GetJsonAsync<IEnumerable<PersonalInfo>>($"api/Persons/GetEmployees?EmployeeName=" + EmployeeName + "&expertiseID=" + expertiseID + "&competencyID=" + competencyID);
        }
    }
}
