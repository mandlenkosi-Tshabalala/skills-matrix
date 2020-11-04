using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Expertise 
    {
        public Expertise()
        {
            UserExpertises = new HashSet<UserExpertise>();
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string IPAddress { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<UserExpertise> UserExpertises { get; set; }

    }
}
