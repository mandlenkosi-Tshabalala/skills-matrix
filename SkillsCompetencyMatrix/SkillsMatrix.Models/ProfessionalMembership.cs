using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class ProfessionalMembership : BaseEntity
    {
        public string Membership { get; set; }

        // Foreign key
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
