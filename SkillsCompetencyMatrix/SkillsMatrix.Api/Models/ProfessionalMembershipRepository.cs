using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class ProfessionalMembershipRepository : GenericRepository<Membership>, IProfessionalMembershipRepository
    {
        private readonly AppDbContext appDbContext;

        public ProfessionalMembershipRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
