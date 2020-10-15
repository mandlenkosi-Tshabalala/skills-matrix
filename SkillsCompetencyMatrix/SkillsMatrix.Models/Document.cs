using System;
using System.Collections.Generic;

namespace SkillsMatrix.Models
{
    public class Document:BaseEntity
    {
        public Document()
        {
            UserDocuments = new HashSet<UserDocument>();
        }

        // Foreign key
        public virtual ICollection<UserDocument> UserDocuments { get; set; }
    }
}
