using System;

namespace SkillsMatrix.Models
{
    public class Expertise : BaseEntity
    {
        public string Name { get; set; }

        // Foreign keys
        public int ExpertiseId { get; set; }
        public int PersonId { get; set; }

        // Navigation properties
        public virtual Person Person { get; set; }
        public virtual ExpertiseCategory Catagory { get; set; }
    }
}
