using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public interface IUserSkillsRepository : IGenericRepository<Skill>
    {
        Task<IEnumerable<Skill>> GetUserSkills(int UserID);
    }
}
