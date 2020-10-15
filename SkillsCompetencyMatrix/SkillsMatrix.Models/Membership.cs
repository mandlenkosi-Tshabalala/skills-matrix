using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Membership : BaseEntity
    {
        public Membership()
        {
            UserMemberships = new HashSet<UserMembership>();
        }

        public string Description { get; set; }

        // Foreign key
        public virtual ICollection<UserMembership> UserMemberships { get; set; }
    }
}
