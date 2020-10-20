
using SkillsMatrix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SkillsMatrix.Web.Services
{
    public  interface IPersonExpertiseService
    {
      Task<Expertise> Get(int Id);
      Task<Expertise> Create(Expertise Expertise);
      Task<Expertise> Update(Expertise Expertise);
      Task<IEnumerable<Expertise>> GetExpertiseCategories();

    }
}
