using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Skills: BaseEntity
    {
        public string Name { get; set; }

        // Foreign key
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}