using Microsoft.AspNetCore.Components;
using SkillsMatrix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services.Shared
{
    public class PercentageCalc : IPercentageCalc
    {
        protected PersonalInfo Person = new PersonalInfo();
        protected Address address = new Address();
        protected List<Skill> skills = new List<Skill>();
        protected List<Education> educations = new List<Education>();
        protected List<Employment> employments = new List<Employment>();
        protected List<UserActivities> activities = new List<UserActivities>();
        protected List<UserCompetency> userCompetencies = new List<UserCompetency>();
        protected List<UserExpertise> expertises = new List<UserExpertise>();


        private readonly IPersonService personService;
        private readonly IActivityService ActivityService;
        private readonly ISkillsService SkillsService;
        private readonly IPersonCompetencies PersonCompetencies;
        private readonly IPersonExpertiseService PersonExpertiseService;
        private readonly IAddressService AddressService;
        private readonly IEducationService educationService;
        private readonly IEmployementHistoryService employementHistoryService;


        public PercentageCalc(IPersonService personService, IActivityService ActivityService, ISkillsService SkillsService, IPersonCompetencies PersonCompetencies, IPersonExpertiseService PersonExpertiseService, IAddressService AddressService, IEducationService educationService, IEmployementHistoryService employementHistoryService)
        {
            this.personService = personService;
            this.ActivityService = ActivityService;
            this.SkillsService = SkillsService;
            this.PersonCompetencies = PersonCompetencies;
            this.PersonExpertiseService = PersonExpertiseService;
            this.AddressService = AddressService;
            this.educationService = educationService;
            this.employementHistoryService = employementHistoryService;
        }

        public async Task<int> ProfileCompletion(int UserId)
        {
            int Total = 0;
            int t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0;

            Person = await personService.GetPersonByUserId(UserId);
            address = await AddressService.Get(UserId);
            skills = await SkillsService.GetSkills(UserId);
            educations = await educationService.GetEducations(UserId);
            employments = await employementHistoryService.GetEmployment(UserId);
            activities = await ActivityService.GetActivity(UserId);
            expertises = await PersonExpertiseService.GetAll(UserId);
            userCompetencies = await PersonCompetencies.GetAll(UserId);

            if (Person != null)
            {
                t1 = 20;
            }
            if (address.Id != 0)
            {
                t2 = 20;
            }
            if (educations.Count != 0)
            {
                t3 = 10;
            }
            if (employments.Count != 0)
            {
                t4 = 10;
            }
            if (Person.ImagePath != null)
            {
                t5 = 10;
            }
            if (expertises.Count != 0)
            {
                t6 = 10;
            }
            if (userCompetencies.Count != 0)
            {
                t7 = 10;
            }
            if (skills.Count != 0)
            {
                t8 = 10;
            }

            Total = t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8;

            return Total;
        }
    }
}
