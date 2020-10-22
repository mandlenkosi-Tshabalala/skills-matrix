using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public string Level { get; set; }

    }
}