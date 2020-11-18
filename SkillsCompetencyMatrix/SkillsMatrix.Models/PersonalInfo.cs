using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class PersonalInfo : BaseEntity
    {
        public PersonalInfo()
        {
        }

        [Required]
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public long IdNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Profile { get; set; }

        public int CvProgress{ get; set; }

        public string ImagePath { get; set; }

        [StringLength(250)]
        public string PlaceOfBirth { get; set; }

        [StringLength(50)]
        public string MaritalStatus { get; set; }

     

    }
}
