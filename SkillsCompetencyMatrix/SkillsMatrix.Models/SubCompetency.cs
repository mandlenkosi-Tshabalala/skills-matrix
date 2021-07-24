using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class SubCompetency 
    {
        public SubCompetency()
        {
            UserSubCompetencies = new HashSet<UserSubCompetency>();
        }

        

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string IPAddress { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public int CompetencyId { get; set; }
        public virtual Competency Competency { get; set; }
        public virtual ICollection<UserSubCompetency> UserSubCompetencies { get; set; }
    }
}
