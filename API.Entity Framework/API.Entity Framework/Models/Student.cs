﻿using System.ComponentModel.DataAnnotations;

namespace API.Entity_Framework.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
    }
}
