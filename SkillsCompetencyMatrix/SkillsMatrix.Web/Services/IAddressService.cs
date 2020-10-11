using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddresses();
    }
}
