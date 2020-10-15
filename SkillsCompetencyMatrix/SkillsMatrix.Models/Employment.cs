using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Employment : BaseEntity
    {
        public Employment()
        {
            UserEmployments = new HashSet<UserEmployment>();
        }

        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CurrentlyWorking { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public String RoleDescription { get; set; }

        // Foreign key
        public virtual ICollection<UserEmployment> UserEmployments { get; set; }
    }
}

