using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class ProfessionalMembership
    {
        public int Id { get; set; }
        public string Membership { get; set; }
        public bool IsDeleted { get; set; }
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }
    }
}
