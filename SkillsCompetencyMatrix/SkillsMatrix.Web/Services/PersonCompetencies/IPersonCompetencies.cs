
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public  interface IPersonCompetencies
    {
      Task<UserCompetency> Get(int Id);
      Task<UserCompetency> Create(UserCompetency competency);
      Task<UserCompetency> Update(UserCompetency competency);

       Task<IEnumerable<UserCompetency>> GetCompetencies();

        Task<List<UserCompetency>> GetAll(int UserID);
        Task Delete(int Id);

    }
}
