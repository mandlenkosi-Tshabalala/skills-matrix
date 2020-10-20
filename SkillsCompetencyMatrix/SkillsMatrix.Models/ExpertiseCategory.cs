using System;

namespace SkillsMatrix.Models
{
    public class ExpertiseCategory 
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string IPAddress { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
    }
}
