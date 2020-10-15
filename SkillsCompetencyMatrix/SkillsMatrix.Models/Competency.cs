using System;
using System.Collections.Generic;

namespace SkillsMatrix.Models
{
    public class Competency : BaseEntity
    {
        public Competency()
        {
            UserCompetencies = new HashSet<UserCompetency>();
        }
        public string Name { get; set; }

        // Foreign key
        public int CatagoryId { get; set; }

        // Navigation properties
        public virtual ICollection<UserCompetency> UserCompetencies { get; private set; }
        public virtual CompetencyCategory Catagory { get; set; }
    }
}
