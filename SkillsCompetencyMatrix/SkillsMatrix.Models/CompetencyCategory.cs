using System;
using System.Collections.Generic;

namespace SkillsMatrix.Models
{
    public class CompetencyCategory : BaseEntity
    {
        public CompetencyCategory()
        {
            Competencies = new HashSet<Competency>();
        }

        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<Competency> Competencies { get; private set; }
    }
}
