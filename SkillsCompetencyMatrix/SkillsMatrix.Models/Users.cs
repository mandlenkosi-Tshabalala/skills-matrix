using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserLastName { get; set; }
        public bool IsDeleted { get; set; }
    }
}