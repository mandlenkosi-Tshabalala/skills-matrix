﻿using System;
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
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }

        public int UserId { get; set; } 
        public virtual User User { get; set; }

    }
}
