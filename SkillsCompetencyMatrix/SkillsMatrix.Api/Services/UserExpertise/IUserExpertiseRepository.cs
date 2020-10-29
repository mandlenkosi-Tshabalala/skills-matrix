using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public interface IUserExpertiseRepository : IGenericRepository<UserExpertise>
    {
        Task<UserExpertise> GetByUserAndExpertiseId(int UserId, int ExpertiseId);
        Task<IEnumerable<UserExpertise>> GetUserExpertise(int UserID);
        Task<UserExpertise> DeleteByUserAndExpertiseId(int UserId, int ExpertiseId);
        Task<UserExpertise> UnDeleteByUserAndExpertiseId(int UserId, int ExpertiseId);
    }
}
