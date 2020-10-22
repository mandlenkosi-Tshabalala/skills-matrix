using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public interface IEducationRepository : IGenericRepository<Education>
    {
        Task<IEnumerable<Education>> Search(string fieldOfStudy, string qualificationLevel);
        Task<IEnumerable<Education>> GetEducations(int UserID);
    }
}
