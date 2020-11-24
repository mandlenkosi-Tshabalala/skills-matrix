using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http.Extensions;
using System.Web;
using System.Net.Http.Json;

namespace SkillsMatrix.Web.Services
{
    public class PersonService : IPersonService
    {


        [Inject]
        public IPersonService PersonInfoService { get; set; }
        [Inject]
        public IActivityService ActivityService { get; set; }
        

        [Inject]
        public ISkillsService SkillsService { get; set; }
        [Inject]
        public IPersonService personService { get; set; }

        [Inject]
        public IPersonCompetencies PersonCompetencies { get; set; }

        [Inject]
        public IPersonExpertiseService PersonExpertiseService { get; set; }

        [Inject]
        public IAddressService AddressService { get; set; }
        [Inject]
        public IEducationService educationService { get; set; }

        [Inject]
        public IEmployementHistoryService employementHistoryService { get; set; }

        protected PersonalInfo Person = new PersonalInfo();
        protected Address address = new Address();
        protected List<Skill> skills = new List<Skill>();
        protected List<Education> educations = new List<Education>();
        protected List<Employment> employments = new List<Employment>();
        protected List<UserActivities> activities = new List<UserActivities>();
        protected List<UserCompetency> competencies = new List<UserCompetency>();
        protected List<UserExpertise> expertises = new List<UserExpertise>();

        public readonly HttpClient httpClient;

        public PersonService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PersonalInfo> Create(PersonalInfo person)
        {
            return await httpClient.PostJsonAsync<PersonalInfo>($"api/Persons", person);
        }

        public async Task<PersonalInfo> GetPerson(int Id)
        {
           return await httpClient.GetJsonAsync<PersonalInfo>($"api/Persons/{Id}");
        }

        public async Task<PersonalInfo> Update(PersonalInfo person)
        {
            return await httpClient.PutJsonAsync<PersonalInfo>($"api/Persons", person);
        }
        public async Task<IEnumerable<PersonalInfo>> GetAllEmployees()
        {
            return await httpClient.GetJsonAsync<IEnumerable<PersonalInfo>>($"api/Persons/GetEmployees");
        }


        public async Task<PersonalInfo> GetPersonByUserId(int Id)
        {
            return await httpClient.GetJsonAsync<PersonalInfo>($"api/Persons/GetByUserId/{Id}");
        }


        public async Task<IEnumerable<PersonalInfo>> GetAllEmployees(string EmployeeName, int expertiseID, int competencyCategoryID, string Skills, string QualificationLevel, string Country, int competencyID)
        {
            return await httpClient.GetJsonAsync<IEnumerable<PersonalInfo>>($"api/Persons/GetEmployees?EmployeeName=" + HttpUtility.UrlEncode(EmployeeName) + "&expertiseID=" + expertiseID + "&competencyCategoryID=" + competencyCategoryID + "&Skills=" + HttpUtility.UrlEncode(Skills) + "&QualificationLevel=" + QualificationLevel + "&Country=" + Country + "&competencyID=" + competencyID);
        }

        public async Task GetCV(int intId)
        {
             await httpClient.GetJsonAsync<PersonalInfo>($"api/Persons/downloadCV/{intId}");
        }
        public async Task UpdatePercentageComletion(int userId, int Percentage)
        {
            await httpClient.PutJsonAsync($"api/Persons/UpdateCompletion/{userId}/{Percentage}", "");
        }
    }
}
