
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public  interface IPersonCompetencies
    {
      Task<Competency> Get(int Id);
      Task<Competency> Create(Competency competency);
      Task<Competency> Update(Competency competency);
      Task<IEnumerable<Competency>> GetCompetencies();


    }
}
