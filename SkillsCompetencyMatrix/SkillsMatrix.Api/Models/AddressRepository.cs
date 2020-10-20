using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly AppDbContext appDbContext;

        public AddressRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Task<Address> GetByUserId(int Id) 
        { 
        return appDbContext.Addresses.SingleOrDefaultAsync(x => x.UserId== Id);
        }
    }
}
