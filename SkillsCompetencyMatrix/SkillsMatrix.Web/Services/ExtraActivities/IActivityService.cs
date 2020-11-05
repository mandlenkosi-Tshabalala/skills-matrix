
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
namespace SkillsMatrix.Web.Services
{
    public  interface IActivityService
    {
      Task<UserActivities> Get(int Id);
      Task<UserActivities> Create(UserActivities activity);
      Task<UserActivities> Update(UserActivities activity);
      Task<List<UserActivities>> GetActivity(int UserID);
      Task Delete(int Id);

    }
}
