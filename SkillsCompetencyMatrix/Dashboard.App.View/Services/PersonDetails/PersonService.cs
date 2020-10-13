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
    public class PersonService : IPersonService
    {

        public readonly HttpClient httpClient;

        public PersonService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Person> Create(Person person)
        {
            return await httpClient.PostJsonAsync<Person>($"api/Person", person);
        }

        public async Task<Person> GetPerson(int Id)
        {
           return await httpClient.GetJsonAsync<Person>($"api/Person/{Id}");
        }

        public async Task<Person> Update(Person person)
        {
            return await httpClient.PutJsonAsync<Person>($"api/Person", person);
        }
    }
}
