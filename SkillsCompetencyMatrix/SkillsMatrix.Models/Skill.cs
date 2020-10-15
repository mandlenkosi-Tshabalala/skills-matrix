using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Skill : BaseEntity
    {
        public Skill()
        {
            UserSkills = new HashSet<UserSkill>();
        }
        public string Name { get; set; }
        public string Level { get; set; }

        // Foreign key
        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}