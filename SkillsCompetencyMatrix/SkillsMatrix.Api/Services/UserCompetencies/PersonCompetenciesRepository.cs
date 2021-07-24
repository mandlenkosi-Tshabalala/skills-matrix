using Microsoft.EntityFrameworkCore;
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

        public async Task<UserCompetency> GetByUserAndCompetencyId(int UserId, int Id)
        {
            var query = await appDbContext.UserCompetencies.SingleOrDefaultAsync(x => x.UserId == UserId && x.CompetencyId == Id);
            //UserCompetency result = query;
            var subCompetencies = appDbContext.SubCompetencies.Where(a=>a.CompetencyId== Id);
            var userSubCompetencies = new List<UserSubCompetency>();
            foreach (var subCompetency in subCompetencies.ToList<SubCompetency>())
            {
                var _userSubCompetencies = appDbContext.UserSubCompetencies.Where(a => a.SubCompetencyId== subCompetency.Id && a.UserId==UserId).ToList<UserSubCompetency>();
                foreach (var _userSubCompetency in _userSubCompetencies)
                {
                    userSubCompetencies.Add(_userSubCompetency);
                }
                
            }
            if (query!=null && query.UserSubCompetencies != null)
            {
                query.UserSubCompetencies = userSubCompetencies;
            }
            
            return query;
        }


        public async Task<IEnumerable<UserCompetency>> GetUserCompetences(int UserID)
        {

            IQueryable<UserCompetency> query = appDbContext.UserCompetencies.Include(a => a.Competency).Where(e => e.UserId == UserID && e.IsDeleted == false); ;
            var result = query.ToList<UserCompetency>();
            foreach (var userCompetency in result)
            {
                var query2 = appDbContext.SubCompetencies.Where(a => a.CompetencyId == userCompetency.CompetencyId);
                var subCompetencies = await query2.ToListAsync<SubCompetency>();
                var userSubCompetencies = new List<UserSubCompetency>();
                foreach (var subCompetency in subCompetencies)
                {
                    var _userSubCompetency = appDbContext.UserSubCompetencies.Where(a => a.SubCompetencyId == subCompetency.Id);
                    if (_userSubCompetency != null)
                    {
                        userSubCompetencies.AddRange(_userSubCompetency.ToList<UserSubCompetency>());
                    }

                }
                userCompetency.UserSubCompetencies = userSubCompetencies;
            }
                
            
            return result;

        }

        public async Task<UserCompetency> DeleteUserCompetencyId(int UserId, int Id)
        {
            var userCompetency = await appDbContext.UserCompetencies.SingleOrDefaultAsync(x => x.UserId == UserId && x.CompetencyId == Id);
            userCompetency.IsDeleted = true;
            var subCompetencies = appDbContext.SubCompetencies.Where(a => a.CompetencyId == userCompetency.CompetencyId);
            foreach (var subCompetency in subCompetencies.ToList<SubCompetency>())
            {
                var userSubCompetencies = appDbContext.UserSubCompetencies.Where(b => b.SubCompetencyId == subCompetency.Id && b.UserId == UserId);
                foreach (var userSubCompetency in userSubCompetencies.ToList<UserSubCompetency>())
                {
                    userSubCompetency.IsDeleted = true;
                    appDbContext.Set<UserSubCompetency>().Update(userSubCompetency);
                    await appDbContext.SaveChangesAsync();
                }
            }
            return await Update(userCompetency);
        }

        public async Task<UserCompetency> UnDeleteCompetencyId(int UserId, int Id)
        {
            var userCompetency = await appDbContext.UserCompetencies.SingleOrDefaultAsync(x => x.UserId == UserId && x.CompetencyId == Id);
            userCompetency.IsDeleted = false;
            var subCompetencies = appDbContext.SubCompetencies.Where(a=>a.CompetencyId==userCompetency.CompetencyId);
            foreach (var subCompetency in subCompetencies.ToList<SubCompetency>())
            {
                var userSubCompetencies = appDbContext.UserSubCompetencies.Where(b => b.SubCompetencyId==subCompetency.Id && b.UserId==UserId);
                foreach (var userSubCompetency in userSubCompetencies.ToList<UserSubCompetency>())
                {
                    userSubCompetency.IsDeleted = false;
                    appDbContext.Set<UserSubCompetency>().Update(userSubCompetency);
                    await appDbContext.SaveChangesAsync();
                }
            }
            
            return await Update(userCompetency);
        }

      
        public async Task<UserCompetency> AddUserCompetency(UserCompetency userCompetency)
        {
            var result = await appDbContext.UserCompetencies.AddAsync(userCompetency);

            foreach (var id in userCompetency.SubCompetencyIds)
            {
                var userSubCompetency = new UserSubCompetency();
                userSubCompetency.SubCompetencyId = id;
                userSubCompetency.UserId = userCompetency.UserId;

                var result2 = await appDbContext.UserSubCompetencies.AddAsync(userSubCompetency);
            }

            try 
            {

                await appDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
            
            return result.Entity;
        }
    }
}
