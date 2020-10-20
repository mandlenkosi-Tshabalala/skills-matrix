
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public  interface IEducationService
    {
      Task<Education> Get(int Id);
      Task<Education> Create(Education education);
      Task<Education> Update(Education education);
      Task<List<Education>> GetEducations(int UserID);


    }
}
