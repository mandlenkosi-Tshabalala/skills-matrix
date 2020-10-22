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

            //appDbContext.UserExpertises.Where(e => e.UserId == UserID);
            IQueryable<UserExpertise> query = appDbContext.UserExpertises.Include(a => a.Expertise).Where(e => e.UserId == UserID);



            return await query.ToListAsync();

        }
    }
}
