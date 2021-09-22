using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Skill : BaseEntity
    {
        [Required(ErrorMessage = "Name is required is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Level is required is required.")]
        public int Level { get; set; }

    }
}