using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class PersonCompetenciesRepository : GenericRepository<UserCompetency>, IPersonCompetenciesRepository
    {
        private readonly AppDbContext appDbContext;

        public PersonCompetenciesRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<UserCompetency> GetByUserAndCompetencyId(int UserId, int Id)
        {
            return await appDbContext.UserCompetencies.SingleOrDefaultAsync(x => x.UserId == UserId && x.CompetencyId == Id);
        }


        public async Task<IEnumerable<UserCompetency>> GetUserCompetences(int UserID)
        {

            IQueryable<UserCompetency> query = appDbContext.UserCompetencies.Include(a => a.Competency).Where(e => e.UserId == UserID && e.IsDeleted == false); ;
            return await query.ToListAsync();

        }

        public async Task<UserCompetency> DeleteUserCompetencyId(int UserId, int Id)
        {
            var ue = await appDbContext.UserCompetencies.SingleOrDefaultAsync(x => x.UserId == UserId && x.CompetencyId == Id);
            ue.IsDeleted = true;
            return await Update(ue);
        }

        public async Task<UserCompetency> UnDeleteCompetencyId(int UserId, int Id)
        {
            var ue = await appDbContext.UserCompetencies.SingleOrDefaultAsync(x => x.UserId == UserId && x.CompetencyId == Id);
            ue.IsDeleted = false;
            return await Update(ue);
        }
    }
}
