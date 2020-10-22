
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public  interface ICompetenciesCategoryService
    {
      Task<CompetencyCategory> Get(int Id);
      Task<CompetencyCategory> Create(CompetencyCategory competency);
      Task<CompetencyCategory> Update(CompetencyCategory competency);

      Task<IEnumerable<CompetencyCategory>> GetCompetencies();

      Task<List<CompetencyCategory>> GetAll(int UserID);
        Task Delete(int Id);

    }
}
