using Microsoft.AspNetCore.Components;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient httpClient;

        public AddressService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await httpClient.GetJsonAsync<Address[]>("api/addresses");
        }
    }
}
