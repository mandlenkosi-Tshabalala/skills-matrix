
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public  interface ICompetenciesService
    {
      Task<Competency> Get(int Id);
      Task<Competency> Create(Competency competency);
      Task<Competency> Update(Competency competency);

       Task<IEnumerable<Competency>> GetCompetencies();

        Task<List<Competency>> GetAll(int UserID);
        Task Delete(int Id);

    }
}
