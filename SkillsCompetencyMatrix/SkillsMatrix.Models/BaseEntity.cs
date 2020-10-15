using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SkillsMatrix.Models
{
    public class BaseEntity
    {
        // Primary key
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
        public bool IsDeleted { get; set; }
    }
}
