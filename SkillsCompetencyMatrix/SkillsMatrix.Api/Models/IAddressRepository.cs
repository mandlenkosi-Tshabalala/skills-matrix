using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddresses();
        Task<Address> GetAddress(int addressId);
        Task<Address> AddAddress(Address address);
        Task<Address> UpdateAddress(Address address);
        Task<Address> DeleteAddress(int addressId);
        Task<IEnumerable<Address>> Search(string code, string addressline);
    }
}
