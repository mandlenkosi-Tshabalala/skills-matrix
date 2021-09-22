using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Employment : BaseEntity
    {

        [Required(ErrorMessage = "Company Name is required is required.")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Role is required is required.")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Start date is required is required.")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public bool CurrentlyWorking { get; set; }
        [Required(ErrorMessage = "Country is required is required.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Role description is required is required.")]
        public String RoleDescription { get; set; }

    }
}

