using System;

namespace SkillsMatrix.Models
{
    public class Expertise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExpertiseId { get; set; }
        public virtual ExpertiseCategory Catagory { get; set; }
        public bool IsDeleted { get; set; }
    }
}
