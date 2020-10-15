using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Reference : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        // Foreign key
        public int PersonId { get; set; }
        public virtual PersonalInfo Person { get; set; }
    }
}
