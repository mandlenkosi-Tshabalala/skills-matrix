
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public  interface IEducationService
    {
      Task<Education> Get(int Id);
      Task<Education> Create(Education education);
      Task<Education> Update(Education education);

    }
}
