
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public  interface IPersonService
    {
      Task<PersonalInfo> GetPerson(int Id);
      Task<PersonalInfo> Create(PersonalInfo person);
      Task<PersonalInfo> Update(PersonalInfo person);
      Task<IEnumerable<PersonalInfo>> GetAllEmployees(string EmployeeName, int expertiseID, int competencyID);

    }
}
