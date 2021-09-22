using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Membership : BaseEntity
    {
        [Required(ErrorMessage = "Description is required is required.")]
        public string Description { get; set; }
 
    }
}
