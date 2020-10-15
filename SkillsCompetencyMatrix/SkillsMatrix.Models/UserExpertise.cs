using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserExpertise
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExpertiseId { get; set; }
        public Expertise Expertise { get; set; }
    }
}
