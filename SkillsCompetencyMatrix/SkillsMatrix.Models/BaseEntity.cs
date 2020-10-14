using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class BaseEntity
    {
        // Primary key
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
        public bool IsDeleted { get; set; }
    }
}
