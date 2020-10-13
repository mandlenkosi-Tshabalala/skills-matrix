
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public  interface IEmployementHistoryService
    {
      Task<EmploymentHistory> Get(int Id);
      Task<EmploymentHistory> Create(EmploymentHistory person);
      Task<EmploymentHistory> Update(EmploymentHistory person);

    }
}
