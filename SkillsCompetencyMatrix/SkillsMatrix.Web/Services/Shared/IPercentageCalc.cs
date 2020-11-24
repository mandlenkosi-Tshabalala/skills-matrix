using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services.Shared
{
  public  interface IPercentageCalc
    {
        Task<int> ProfileCompletion(int UserId);
    }
}
