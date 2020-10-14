using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Industry : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        // Foreign key
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
