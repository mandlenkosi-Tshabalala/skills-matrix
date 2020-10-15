using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserIndustry
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int IndustryId { get; set; }
        public Industry Industry { get; set; }
    }
}
