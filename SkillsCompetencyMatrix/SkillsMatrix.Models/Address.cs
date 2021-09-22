using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Address : BaseEntity
    {
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Zip code is required.")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }


    }
}
