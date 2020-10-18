
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services
{
    public  interface IEmployementHistoryService
    {
      Task<Employment> Get(int Id);
      Task<Employment> Create(Employment person);
      Task<Employment> Update(Employment person);

    }
}
