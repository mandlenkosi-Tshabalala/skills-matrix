using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly AppDbContext appDbContext;

        public PersonRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
