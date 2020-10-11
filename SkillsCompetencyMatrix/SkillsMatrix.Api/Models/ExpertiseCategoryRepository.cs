using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class ExpertiseCategoryRepository : GenericRepository<ExpertiseCategory>, IExpertiseCategoryRepository
    {
        private readonly AppDbContext appDbContext;

        public ExpertiseCategoryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
