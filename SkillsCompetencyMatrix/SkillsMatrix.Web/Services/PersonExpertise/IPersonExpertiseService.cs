
using SkillsMatrix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SkillsMatrix.Web.Services
{
    public  interface IPersonExpertiseService
    {
      Task<UserExpertise> Get(int Id);
      Task<UserExpertise> Create(UserExpertise Expertise);
      Task<UserExpertise> Update(UserExpertise Expertise);
      Task<IEnumerable<UserExpertise>> GetExpertiseCategories();
      Task<List<UserExpertise>> GetAll(int UserID);
      Task Delete(int Id);

    }
}
