using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class ExpertiseRepository : GenericRepository<Expertise>, IExpertiseRepository
    {
        private readonly AppDbContext appDbContext;

        public ExpertiseRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
