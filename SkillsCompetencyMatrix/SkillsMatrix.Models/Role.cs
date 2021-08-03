using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkillsMatrix.Models
{
    public class Role
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        [MaxLength(256)]
        public string NormalizedName { get; set; }

        public string ConcurrencyStamp { get; set; }
        //public virtual ICollection<UserRole> UserRoles { get; private set; }
    }
}
