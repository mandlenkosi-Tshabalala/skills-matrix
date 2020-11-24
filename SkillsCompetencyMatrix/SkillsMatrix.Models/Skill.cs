using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Skill : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Level { get; set; }

    }
}