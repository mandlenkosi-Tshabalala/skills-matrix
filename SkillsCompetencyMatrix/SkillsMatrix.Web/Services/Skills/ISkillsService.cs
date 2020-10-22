
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace SkillsMatrix.Web.Services
{
    public  interface ISkillsService
    {
      Task<Skill> Get(int Id);
      Task<Skill> Create(Skill skill);
      Task<Skill> Update(Skill skill);
      Task<List<Skill>> GetSkills(int UserID);
      Task Delete(int Id);

    }
}
