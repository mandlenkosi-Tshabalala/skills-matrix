using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public  interface IAddressService
    {
      Task<Person> Get(int Id);
      Task<Person> Create(Person person);
      Task<Person> Update(Person person);

    }
}
