using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Address : BaseEntity
    {
        [Required]
        public string PhysicalCode { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public string PhysicalAddressLine1 { get; set; }
        public string PostalAddressLine1 { get; set; }
        [Required]
        public string PhysicalAddressLine2 { get; set; }
        public string PostalAddressLine2 { get; set; }
        public string PhysicalAddressLine3 { get; set; }
        public string PostalAddressLine3 { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
    }
}
