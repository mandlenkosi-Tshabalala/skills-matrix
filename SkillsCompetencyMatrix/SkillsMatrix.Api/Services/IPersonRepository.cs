﻿using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public interface IPersonRepository : IGenericRepository<PersonalInfo>
    {
        Task<IEnumerable<PersonalInfo>>GetAllEmployees(string EmployeeName, int expertiseID, int competencyCategoryID, string Skills, string QualificationLevel, string Country);
        Task<PersonalInfo> GetByUserId(int Id);
    }
}
