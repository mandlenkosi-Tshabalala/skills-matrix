using System;

namespace SkillsMatrix.Models
{
    public class Competency : BaseEntity
    {
        public string Name { get; set; }       

        // Foreign key
        public int PersonId { get; set; }
        public int CatagoryId { get; set; }


        // Navigation properties
        public virtual Person Person { get; set; }
        public virtual CompetencyCategory Catagory { get; set; }
    }
}
