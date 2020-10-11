using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillsMatrix.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddresses();
    }
}
