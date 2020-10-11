using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class IndustryRepository : GenericRepository<Industry>, IIndustryRepository
    {
        private readonly AppDbContext appDbContext;

        public IndustryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
