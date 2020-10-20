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

        //public override async Task<User> GetById(int Id)
        //{

        //      var result = await appDbContext.Users.Include(x => x.Address).ThenInclude(y => y.)

        //}
    }
}
