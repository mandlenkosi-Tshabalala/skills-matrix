using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Person : BaseEntity
    {
        public Person()
        {
            this.Addresses = new HashSet<Address>();
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

        // Navigation properties
        public virtual ICollection<Address> Addresses { get; private set; }
        public virtual ICollection<Competency> Competencies { get; private set; }
        public virtual ICollection<Education> Educations { get; private set; }
        public virtual ICollection<EmploymentHistory> EmploymentHistories { get; private set; }
        public virtual ICollection<Expertise> Expertises { get; private set; }
        public virtual ICollection<Industry> Industries { get; private set; }

    }
}
