
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public  interface IProfessionalMembershipService
    {
      Task<ProfessionalMembership> Get(int Id);
      Task<ProfessionalMembership> Create(ProfessionalMembership professionalMembership);
      Task<ProfessionalMembership> Update(ProfessionalMembership professionalMembership);

    }
}
