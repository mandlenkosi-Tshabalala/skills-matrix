using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserMembership
    {
        public int UserId { get; set; }
        public int MembershipId { get; set; }
        public User User { get; set; }
        public Membership Membership { get; set; }
    }
}
