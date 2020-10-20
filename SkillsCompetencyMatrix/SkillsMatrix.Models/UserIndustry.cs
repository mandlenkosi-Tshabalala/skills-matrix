using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserIndustry : BaseEntity
    {
        public int IndustryId { get; set; }
        public virtual Industry Industry { get; set; }
    }
}
