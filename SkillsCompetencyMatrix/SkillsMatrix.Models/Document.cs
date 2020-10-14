using System;

namespace SkillsMatrix.Models
{
    public class Document:BaseEntity
    {
        // Foreign key
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
