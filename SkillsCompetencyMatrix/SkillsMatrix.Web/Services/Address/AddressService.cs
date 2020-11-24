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
    public class AddressService : IAddressService
    {

        public readonly HttpClient httpClient;

        public AddressService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Address> Create(Address PersonAddress)
        {
            return await httpClient.PostJsonAsync<Address>($"api/Addresses", PersonAddress);
        }

        public async Task<Address> Get(int Id)
        {
           return await httpClient.GetJsonAsync<Address>($"api/Addresses/GetByUserId/{Id}");
        }

        public async Task<Address> Update(Address PersonAddress)
        {
            return await httpClient.PutJsonAsync<Address>($"api/Addresses", PersonAddress);
        }



    }
}
