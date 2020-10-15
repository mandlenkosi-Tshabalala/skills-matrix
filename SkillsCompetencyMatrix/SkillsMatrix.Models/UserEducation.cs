using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserEducation
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int EducationId { get; set; }
        public Education Education { get; set; }
    }
}
