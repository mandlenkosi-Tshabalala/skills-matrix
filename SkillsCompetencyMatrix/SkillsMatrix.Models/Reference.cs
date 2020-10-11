using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Reference
    {
        public int Id { get; set; }
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
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }
    }
}
