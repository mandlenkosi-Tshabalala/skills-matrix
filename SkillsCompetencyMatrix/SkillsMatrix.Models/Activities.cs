using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class UserActivities : BaseEntity
    {
        [StringLength(250)]
        [Required]
        public string Activity { get; set; }
        [Required]
        [StringLength(500)]
        public string ActivityDetail { get; set; }

    }
}