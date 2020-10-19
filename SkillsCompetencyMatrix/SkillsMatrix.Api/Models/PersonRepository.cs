using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Api.Models
{
    public class PersonRepository : GenericRepository<PersonalInfo>, IPersonRepository
    {
        private readonly AppDbContext appDbContext;

        public PersonRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<PersonalInfo>> GetAllEmployees(string EmployeeName, int expertiseID, int competencyID)
        {
            //return await appDbContext.PersonalInfos.ToListAsync();

            var employees = from PersonalInfo in appDbContext.PersonalInfos
                            join UserExpertise in appDbContext.UserExpertises on PersonalInfo.UserId equals UserExpertise.UserId into ue
                            from subUserExpertise in ue.DefaultIfEmpty()
                            join UserCompetency in appDbContext.UserCompetencies on PersonalInfo.UserId equals UserCompetency.UserId into uc
                            from subUserCompetency in uc.DefaultIfEmpty()
                            where
                            ((!String.IsNullOrEmpty(EmployeeName) && (PersonalInfo.FirstName + PersonalInfo.MiddleName + PersonalInfo.LastName).Contains(EmployeeName)) || String.IsNullOrEmpty(EmployeeName))
                            && ((expertiseID != 0 && subUserExpertise.ExpertiseId == expertiseID) || (expertiseID == 0))
                            && ((competencyID != 0 && subUserCompetency.CompetencyId == competencyID) || (competencyID == 0))
                            && PersonalInfo.IsDeleted == false
                            select PersonalInfo;

            return await employees.Distinct().ToListAsync();
        }

    }
}