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

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "ID number is required.")]
        public long IdNumber { get; set; }
        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Nationality is required.")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Profile is required.")]
        public string Profile { get; set; }

        public int CvProgress{ get; set; }

        public string ImagePath { get; set; }

        [StringLength(250, ErrorMessage = "The {0} must be {1} characters long.")]
        public string PlaceOfBirth { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be {1} characters long.")]
        public string MaritalStatus { get; set; }

     

    }
}
