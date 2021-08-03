using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace SkillsMatrix.Models
{
    public class User
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
            //UserRoles = new HashSet<UserRole>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string UserName { get; set; }

        [MaxLength(256)]
        public string NormalizedUserName { get; set; }

        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(256)]
        public string NormalizedEmail { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public bool PhoneNumberConfirmed { get; set; }

        [Required]
        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEnd { get; set; }

        [Required]
        public bool LockoutEnabled { get; set; }

        [Required]
        public int AccessFailedCount { get; set; }

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
        //public virtual ICollection<UserRole> UserRoles { get; private set; }
    }
}