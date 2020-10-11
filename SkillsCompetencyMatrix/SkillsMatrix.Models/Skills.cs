using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsMatrix.Models
{
    public class Skills
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }
        public bool IsDeleted { get; set; }
    }
}