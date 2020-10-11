using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Industry
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }     
    }
}
