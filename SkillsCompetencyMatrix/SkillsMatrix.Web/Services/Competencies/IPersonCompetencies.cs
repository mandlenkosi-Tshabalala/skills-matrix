
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public  interface IPersonCompetencies
    {
      Task<Competency> Get(int Id);
      Task<Competency> Create(Competency competency);
      Task<Competency> Update(Competency competency);

    }
}
