using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class EmploymentHistoryRepository : GenericRepository<Employment>, IEmploymentHistoryRepository
    {
        private readonly AppDbContext appDbContext;

        public EmploymentHistoryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
