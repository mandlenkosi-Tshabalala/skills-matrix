using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SkillsMatrix.Models
{
    public class Education : BaseEntity
    {

        [Required(ErrorMessage = "Intitution is required is required.")]
        public string Institution { get; set; }
        [Required(ErrorMessage = "Field Of Study is required is required.")]
        public string FieldOfStudy { get; set; }
        [Required(ErrorMessage = "Qualification Start Date is required is required.")]
        public DateTime QualificationStartDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Qualification End Date is required is required.")]
        public DateTime QualificationEndDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Qualification Level is required is required.")]
        public string QualificationLevel { get; set; }
        [Required(ErrorMessage = "Progress is required is required.")]
        public string Progress { get; set; } 

    }
}
