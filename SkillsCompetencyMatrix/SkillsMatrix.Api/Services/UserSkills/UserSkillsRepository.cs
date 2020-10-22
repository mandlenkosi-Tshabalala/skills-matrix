using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class UserSkillsRepository : GenericRepository<Skill>, IUserSkillsRepository
    {
        private readonly AppDbContext appDbContext;

        public UserSkillsRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<IEnumerable<Skill>> GetUserSkills(int UserID)
        {
            IQueryable<Skill> query = appDbContext.Skills.Where(e => e.UserId == UserID);


            return await query.ToListAsync();

        }
    }
}
