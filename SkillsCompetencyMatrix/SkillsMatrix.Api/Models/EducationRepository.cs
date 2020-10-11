using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class EducationRepository : GenericRepository<Education>, IEducationRepository
    {
        private readonly AppDbContext appDbContext;

        public EducationRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Education>> Search(string fieldOfStudy, string qualificationLevel)
        {
            IQueryable<Education> query = appDbContext.Educations;

            if (!string.IsNullOrEmpty(qualificationLevel))
            {
                query = query.Where(e => e.QualificationLevel.Contains(qualificationLevel));
            }

            if (!string.IsNullOrEmpty(fieldOfStudy))
            {
                query = query.Where(e => e.FieldOfStudy.Contains(fieldOfStudy));
            }

            return await query.ToListAsync();
        }
    }
}
