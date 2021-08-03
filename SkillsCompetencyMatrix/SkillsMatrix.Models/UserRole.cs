using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserRole
    {
        
        [Required]
        [Key]
        public int UserId { get; set; }
        [Required]
        [Key]
        public int RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
