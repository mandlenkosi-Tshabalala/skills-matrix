using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserCompetency
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int CompetencyId { get; set; }
        public Competency Competency { get; set; }
    }
}
