using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace SkillsMatrix.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            UserSkills = new HashSet<UserSkill>();
            UserCompetencies = new HashSet<UserCompetency>();
            UserEducations = new HashSet<UserEducation>();
            UserEmployments = new HashSet<UserEmployment>();
            UserExpertises = new HashSet<UserExpertise>();
            UserIndustries = new HashSet<UserIndustry>();
            UserMemberships = new HashSet<UserMembership>();
            UserDocuments = new HashSet<UserDocument>();
        }

        [Required]
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
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
        public virtual ICollection<UserSkill> UserSkills { get; set; }
        public virtual ICollection<UserCompetency> UserCompetencies { get; private set; }
        public virtual ICollection<UserEducation> UserEducations { get; private set; }
        public virtual ICollection<UserEmployment> UserEmployments { get; private set; }
        public virtual ICollection<UserExpertise> UserExpertises { get; private set; }
        public virtual ICollection<UserIndustry> UserIndustries { get; private set; }
        public virtual ICollection<UserMembership> UserMemberships { get; private set; }
        public virtual ICollection<UserDocument> UserDocuments { get; private set; }
    }
}