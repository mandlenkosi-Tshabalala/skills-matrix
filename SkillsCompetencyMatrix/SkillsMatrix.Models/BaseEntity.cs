﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SkillsMatrix.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
    }
}
