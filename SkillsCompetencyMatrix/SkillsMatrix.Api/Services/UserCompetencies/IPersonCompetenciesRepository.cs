using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public interface IPersonCompetenciesRepository : IGenericRepository<UserCompetency>
    {
        Task<IEnumerable<UserCompetency>> GetUserCompetences(int UserID);

        Task<UserCompetency> DeleteUserCompetencyId(int UserId, int Id);

        Task<UserCompetency> UnDeleteCompetencyId(int UserId, int ExpertiseId);

        Task<UserCompetency> GetByUserAndCompetencyId(int UserId, int ExpertiseId);
    }
}
