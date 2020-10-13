
using SkillsMatrix.Models;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Services
{
    public  interface IPersonExpertiseService
    {
      Task<Expertise> Get(int Id);
      Task<Expertise> Create(Expertise Expertise);
      Task<Expertise> Update(Expertise Expertise);

    }
}
