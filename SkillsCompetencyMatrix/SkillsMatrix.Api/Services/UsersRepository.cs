using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        private readonly AppDbContext appDbContext;

        public UsersRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Task<User> GetUserCVById(int Id)
        {

            //appDbContext.UserExpertises.Where(e => e.UserId == UserID);
            // IQueryable<User> query = appDbContext.Addresses.Include(a => a.).Where(e => e.UserId == Id);
            // return await query.ToListAsync();

            return null;
        }

        //public override async Task<User> GetById(int Id)
        //{

        //      var result = await appDbContext.Users.Include(x => x.Address).ThenInclude(y => y.)

        //}
    }
}
