using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SkillsMatrix.Api.Models
{
    public class PersonRepository : GenericRepository<PersonalInfo>, IPersonRepository
    {
        private readonly AppDbContext appDbContext;

        public PersonRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<PersonalInfo>> GetAllEmployees()
        {
            return await appDbContext.PersonalInfos.ToListAsync();
        }
    }
}
