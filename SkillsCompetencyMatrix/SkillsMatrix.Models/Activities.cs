using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class UserActivities : BaseEntity
    {
        [StringLength(250)]
        public string Activity { get; set; }

        [StringLength(500)]
        public string ActivityDetail { get; set; }

    }
}