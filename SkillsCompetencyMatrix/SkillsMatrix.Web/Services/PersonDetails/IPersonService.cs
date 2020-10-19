
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace SkillsMatrix.Web.Services
{
    public  interface IPersonService
    {
      Task<PersonalInfo> GetPerson(int Id);
      Task<PersonalInfo> Create(PersonalInfo person);
      Task<PersonalInfo> Update(PersonalInfo person);
      Task<IEnumerable<PersonalInfo>> GetAllEmployees();

    }
}
