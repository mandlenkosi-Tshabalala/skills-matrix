
using SkillsMatrix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SkillsMatrix.Web.Services
{
    public  interface IExpertiseService
    {
      Task<Expertise> Get(int Id);
      Task<Expertise> Create(Expertise Expertise);
      Task<Expertise> Update(Expertise Expertise);
      Task<List<Expertise>> GetAll();
      Task Delete(int Id);

    }
}
