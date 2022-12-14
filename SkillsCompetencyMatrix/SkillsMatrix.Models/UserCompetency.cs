using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserCompetency :BaseEntity
    {
        public UserCompetency()
        {
            UserSubCompetencies = new HashSet<UserSubCompetency>();
        }
        [Required]
        public int CompetencyId { get; set; }

        public virtual Competency Competency { get; set; }
        [NotMapped]
        public virtual List<int> SubCompetencyIds { get; set; }
        [NotMapped]
        public virtual ICollection<UserSubCompetency> UserSubCompetencies { get; set; }
    }
}
