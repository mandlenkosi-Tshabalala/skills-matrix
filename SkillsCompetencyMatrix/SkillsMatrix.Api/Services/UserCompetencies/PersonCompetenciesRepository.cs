﻿using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class PersonCompetenciesRepository : GenericRepository<UserCompetency>, IPersonCompetenciesRepository
    {
        private readonly AppDbContext appDbContext;

        public PersonCompetenciesRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<IEnumerable<UserCompetency>> GetUserCompetences(int UserID)
        {
            IQueryable<UserCompetency> query = appDbContext.UserCompetencies.Where(e => e.UserId == UserID);


            return await query.ToListAsync();

        }
    }
}