using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> GetUserCVById(int Id);
    }
}
