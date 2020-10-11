using System;
using System.ComponentModel.DataAnnotations;


namespace SkillsMatrix.Models
{
    public class Education
    {
        public int Id { get; set; }
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
        public bool IsDeleted { get; set; }
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }
    }
}
