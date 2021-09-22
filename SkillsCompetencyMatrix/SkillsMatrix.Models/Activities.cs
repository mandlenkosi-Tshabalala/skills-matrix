using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class UserActivities : BaseEntity
    {
        [StringLength(250, ErrorMessage = "The {0} must be {1} characters long.")]
        [Required(ErrorMessage = "Activity is required is required.")]
        public string Activity { get; set; }
        [Required(ErrorMessage = "Activity Detail is required is required.")]
        [StringLength(500, ErrorMessage = "The {0} must be {1} characters long.")]
        public string ActivityDetail { get; set; }

    }
}