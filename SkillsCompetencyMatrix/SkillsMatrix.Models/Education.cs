using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SkillsMatrix.Models
{
    public class Education : BaseEntity
    {

        [Required]
        public string Institution { get; set; }
        [Required]
        public string FieldOfStudy { get; set; }
        [Required]
        public string QualificationStartDate { get; set; }
        [Required]
        public string QualificationEndDate { get; set; }
        [Required]
        public string QualificationLevel { get; set; }

    }
}
