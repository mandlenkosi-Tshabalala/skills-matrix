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

        public async Task<IEnumerable<Competency>> GetAllByID(int Id)
        {

            IQueryable<Competency> query = appDbContext.Competencies.Where(c => c.CatagoryId == Id).Include(b => b.SubCompetencies);
            var list = await query.ToListAsync();

            return list;

        }

        public async Task<IEnumerable<Competency>> GetAllCompetencies()
        {

            IQueryable<Competency> query = appDbContext.Competencies.Include(b=>b.SubCompetencies);


            return await query.ToListAsync();

        }

        public async Task<IEnumerable<Competency>> Search(string name)
        {
            IQueryable<Competency> query = appDbContext.Competencies.Include(b => b.SubCompetencies);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<Competency> GetCompetencyByID(int Id)
        {
            var competency = appDbContext.Competencies.Include(b => b.SubCompetencies).FirstOrDefault(i => i.Id == Id);
            return competency;

        }
    }
}
