
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public  interface IProfessionalMembershipService
    {
      Task<Membership> Get(int Id);
      Task<Membership> Create(Membership professionalMembership);
      Task<Membership> Update(Membership professionalMembership);

    }
}
