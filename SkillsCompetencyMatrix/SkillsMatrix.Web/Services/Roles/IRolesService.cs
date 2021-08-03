
using SkillsMatrix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SkillsMatrix.Web.Services
{
    public interface IRolesService
    {
        Task<Role> Get(int Id);
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);

        Task<IEnumerable<Role>> GetRoles();

        Task<List<Role>> GetAll();
        Task Delete(int Id);

    }
}
