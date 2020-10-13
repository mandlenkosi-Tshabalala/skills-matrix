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
    public class AddressService : IAddressService
    {

        public readonly HttpClient httpClient;

        public AddressService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Person> Create(Person PersonAddress)
        {
            return await httpClient.PostJsonAsync<Person>($"api/Address", PersonAddress);
        }

        public async Task<Person> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Person>($"api/Address/{Id}");
        }

        public async Task<Person> Update(Person PersonAddress)
        {
            return await httpClient.PutJsonAsync<Person>($"api/Address", PersonAddress);
        }
    }
}
