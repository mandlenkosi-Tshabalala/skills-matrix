using System;

namespace SkillsMatrix.Models
{
    public class Competency
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int CatagoryId { get; set; }
        public virtual CompetencyCategory Catagory { get; set; }
        public bool IsDeleted { get; set; }
    }
}
