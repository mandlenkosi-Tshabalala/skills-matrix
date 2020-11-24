using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserCompetency :BaseEntity
    {
        [Required]
        public int CompetencyId { get; set; }

        public virtual Competency Competency { get; set; }
    }
}
