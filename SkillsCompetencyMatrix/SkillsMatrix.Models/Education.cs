using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SkillsMatrix.Models
{
    public class Education : BaseEntity
    {
        public Education()
        {
            UserEducations = new HashSet<UserEducation>();
        }

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

        // Foreign key
        public virtual ICollection<UserEducation> UserEducations { get; set; }
    }
}
