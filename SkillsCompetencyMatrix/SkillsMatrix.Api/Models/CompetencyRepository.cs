using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class CompetencyRepository : GenericRepository<Competency>, ICompetencyRepository
    {
        private readonly AppDbContext appDbContext;

        public CompetencyRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Competency>> Search(string name)
        {
            IQueryable<Competency> query = appDbContext.Competencies;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            return await query.ToListAsync();
        }
    }
}
