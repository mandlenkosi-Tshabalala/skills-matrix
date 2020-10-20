﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace SkillsMatrix.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            UserSkills = new HashSet<Skill>();
            UserCompetencies = new HashSet<UserCompetency>();
            UserEducations = new HashSet<Education>();
            UserEmployments = new HashSet<Employment>();
            UserExpertises = new HashSet<UserExpertise>();
            UserIndustries = new HashSet<UserIndustry>();
            UserMemberships = new HashSet<Membership>();
            UserDocuments = new HashSet<Document>();
        }

        [Required]
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        // Foreign Keys
        public int PersonalInfoId { get; set; }
        public int AddressId { get; set; }

        // Navigation Properties
        public virtual PersonalInfo PersonalInfo { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Skill> UserSkills { get; set; }
        public virtual ICollection<UserCompetency> UserCompetencies { get; private set; }
        public virtual ICollection<Education> UserEducations { get; private set; }
        public virtual ICollection<Employment> UserEmployments { get; private set; }
        public virtual ICollection<UserExpertise> UserExpertises { get; private set; }
        public virtual ICollection<UserIndustry> UserIndustries { get; private set; }
        public virtual ICollection<Membership> UserMemberships { get; private set; }
        public virtual ICollection<Document> UserDocuments { get; private set; }
    }
}