using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserDocument
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
