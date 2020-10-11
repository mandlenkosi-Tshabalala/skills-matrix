using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class ReferenceRepository : GenericRepository<Reference>, IReferenceRepository
    {
        private readonly AppDbContext appDbContext;

        public ReferenceRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
