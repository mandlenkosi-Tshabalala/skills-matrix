using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public  interface IAddressService
    {
      Task<Address> Get(int Id);
      Task<Address> Create(Address address);
      Task<Address> Update(Address address);

    }
}
