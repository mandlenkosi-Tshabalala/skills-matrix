using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class UserExpertiseRepository : GenericRepository<UserExpertise>, IUserExpertiseRepository
    {
        private readonly AppDbContext appDbContext;

        public UserExpertiseRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<IEnumerable<UserExpertise>> GetUserExpertise(int UserID)
        {
            IQueryable<UserExpertise> query = appDbContext.UserExpertises.Include(a => a.Expertise).Where(e => e.UserId == UserID && e.IsDeleted == false); ;

            return await query.ToListAsync();

        }
        public async Task<UserExpertise> GetByUserAndExpertiseId(int UserId, int ExpertiseId)
        {
            return await appDbContext.UserExpertises.SingleOrDefaultAsync(x => x.UserId == UserId && x.ExpertiseId == ExpertiseId) ;
        }
        public async Task<UserExpertise> DeleteByUserAndExpertiseId(int UserId, int ExpertiseId)
        {
            var ue = await appDbContext.UserExpertises.SingleOrDefaultAsync(x => x.UserId == UserId && x.ExpertiseId == ExpertiseId);
            ue.IsDeleted = true;
            return await Update(ue);
        }
        public async Task<UserExpertise> UnDeleteByUserAndExpertiseId(int UserId, int ExpertiseId)
        {
            var ue = await appDbContext.UserExpertises.SingleOrDefaultAsync(x => x.UserId == UserId && x.ExpertiseId == ExpertiseId);
            ue.IsDeleted = false;
            return await Update(ue);
        }
    }
}
