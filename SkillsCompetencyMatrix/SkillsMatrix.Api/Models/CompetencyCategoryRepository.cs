using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class CompetencyCategoryRepository : GenericRepository<CompetencyCategory>, ICompetencyCategoryRepository
    {
        private readonly AppDbContext appDbContext;

        public CompetencyCategoryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
