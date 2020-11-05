using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Api.Migrations;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class ActivityRepository : GenericRepository<UserActivities>, IActivityRepository
    {
        private readonly AppDbContext appDbContext;

        public ActivityRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<UserActivities>> GetUserActivity(int UserId)
        {
            return await appDbContext.UserActivities.Where<UserActivities>(x => x.UserId == UserId).ToListAsync();
        }

    }
}
