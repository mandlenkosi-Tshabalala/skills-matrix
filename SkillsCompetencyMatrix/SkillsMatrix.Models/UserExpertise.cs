using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class UserExpertise : BaseEntity
    {
        public int ExpertiseId { get; set; }

        public virtual Expertise Expertise { get; set; }
    }
}
