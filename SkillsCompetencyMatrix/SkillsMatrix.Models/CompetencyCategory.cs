using System;
using System.Collections.Generic;

namespace SkillsMatrix.Models
{
    public class CompetencyCategory 
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string IPAddress { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }

    }
}
