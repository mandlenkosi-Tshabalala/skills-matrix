using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class SkillsRepository : GenericRepository<Skills>, ISkillsRepository
    {
        private readonly AppDbContext appDbContext;

        public SkillsRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

    }
}
