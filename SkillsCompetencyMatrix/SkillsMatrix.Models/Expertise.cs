using System;
using System.Collections.Generic;

namespace SkillsMatrix.Models
{
    public class Expertise : BaseEntity
    {
        public Expertise()
        {
            UserExpertises = new HashSet<UserExpertise>();
        }

        public string Name { get; set; }

        // Foreign keys
        public int ExpertiseId { get; set; }

        // Navigation properties
        public virtual ExpertiseCategory Catagory { get; set; }
        public virtual ICollection<UserExpertise> UserExpertises { get; set; }
    }
}
