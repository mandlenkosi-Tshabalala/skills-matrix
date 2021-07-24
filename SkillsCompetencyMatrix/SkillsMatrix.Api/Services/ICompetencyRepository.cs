using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public interface ICompetencyRepository : IGenericRepository<Competency>
    {
        Task<IEnumerable<Competency>> Search(string name);
        Task<IEnumerable<Competency>> GetAllByID(int Id);
        Task<IEnumerable<Competency>> GetAllCompetencies();
    }
}
