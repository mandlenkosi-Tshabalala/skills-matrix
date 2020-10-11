using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Address
    {        
        public int Id { get; set; }
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
        public bool IsDeleted { get; set; }
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }
    }
}
