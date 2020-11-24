using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Competency 
    {
        public Competency()
        {
            UserCompetencies = new HashSet<UserCompetency>();
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string IPAddress { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public int CatagoryId { get; set; }
        public virtual CompetencyCategory Catagory { get; set; }

        public virtual ICollection<UserCompetency> UserCompetencies { get; set; }

    }
}
