using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserSubCompetency : BaseEntity
    {
        
        [Required]
        public int SubCompetencyId { get; set; }

        public virtual SubCompetency SubCompetency { get; set; }


    }
}
