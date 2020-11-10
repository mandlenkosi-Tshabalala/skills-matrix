
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace SkillsMatrix.Web.Services
{
    public  interface IPersonService
    {
      Task<PersonalInfo> GetPersonByUserId(int Id);

      Task GetCV(int intId);
      Task<PersonalInfo> GetPerson(int Id);
      Task<PersonalInfo> Create(PersonalInfo person);
      Task<PersonalInfo> Update(PersonalInfo person);
      Task<IEnumerable<PersonalInfo>> GetAllEmployees();
      Task<IEnumerable<PersonalInfo>> GetAllEmployees(string EmployeeName, int expertiseID, int competencyCategoryID, string Skills, string QualificationLevel, string Country, int competencyID);

    }
}
