using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserEmployment
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int EmploymentId { get; set; }
        public Employment Employment { get; set; }
    }
}
