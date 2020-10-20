using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserCompetency :BaseEntity
    {
        public int CompetencyId { get; set; }
        public virtual Competency Competency { get; set; }
    }
}
