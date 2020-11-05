using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

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

        public async Task<IEnumerable<PersonalInfo>> GetAllEmployees(string EmployeeName, int expertiseID, int competencyCategoryID, string Skills, string QualificationLevel, string Country, int competencyID)
        {
            //return await appDbContext.PersonalInfos.ToListAsync();

            var employees = from PersonalInfo in appDbContext.PersonalInfos
                            join UserExpertise in appDbContext.UserExpertises on PersonalInfo.UserId equals UserExpertise.UserId into ue
                            from subUserExpertise in ue.DefaultIfEmpty()
                            join UserCompetency in appDbContext.UserCompetencies on PersonalInfo.UserId equals UserCompetency.UserId into uc
                            from subUserCompetency in uc.DefaultIfEmpty()
                            join Skill in appDbContext.Skills on PersonalInfo.UserId equals Skill.UserId into usk
                            from subUserSkills in usk.DefaultIfEmpty()
                            join Education in appDbContext.Educations on PersonalInfo.UserId equals Education.UserId into use
                            from subUserEducation in use.DefaultIfEmpty()
                            join Employement in appDbContext.Employments on PersonalInfo.UserId equals Employement.UserId into usemp
                            from subUserEmployment in usemp.DefaultIfEmpty()
                            join Competency in appDbContext.Competencies on subUserCompetency.CompetencyId equals Competency.Id into comp
                            from subCompetency in comp.DefaultIfEmpty()
                            where
                            ((!String.IsNullOrEmpty(EmployeeName) && (PersonalInfo.FirstName + PersonalInfo.LastName).Contains(EmployeeName)) || String.IsNullOrEmpty(EmployeeName))
                            && ((expertiseID != 0 && subUserExpertise.ExpertiseId == expertiseID) || (expertiseID == 0))
                            && ((competencyCategoryID != 0 && subCompetency.CatagoryId == competencyCategoryID) || (competencyCategoryID == 0))
                            && ((competencyID != 0 && subUserCompetency.CompetencyId == competencyID) || (competencyID == 0))
                            && PersonalInfo.IsDeleted == false
                            && ((!String.IsNullOrEmpty(Skills) && (subUserSkills.Name).Contains(Skills)) || String.IsNullOrEmpty(Skills))
                            && ((!String.IsNullOrEmpty(QualificationLevel) && (subUserEducation.QualificationLevel).Equals(QualificationLevel)) || String.IsNullOrEmpty(QualificationLevel))
                            && ((!String.IsNullOrEmpty(Country) && (subUserEmployment.Country).Equals(Country)) || String.IsNullOrEmpty(Country))
                            select PersonalInfo;

            return await employees.Distinct().ToListAsync();
        }

        public Task<PersonalInfo> GetByUserId(int Id)
        {
            return appDbContext.PersonalInfos.SingleOrDefaultAsync(x => x.UserId== Id);
        }
    }
}