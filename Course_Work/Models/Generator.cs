using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Course_Work.Models
{
    public class Generator
    {
        [Range(1, 50)]
        [Required]
        public int FromVariables { get; set; }
        [Range(50, 300)]
        [Required]
        public int ToVariables { get; set; }
        [Range(-100, -1)]
        [Required]
        public int FromC { get; set; }
        [Range(1, 50)]
        [Required]
        public int Step { get; set; }
        [Range(1, 200)]
        [Required]
        public int ToC { get; set; }
        [Range(1, 150)]
        [Required]
        public int Number { get; set; }    
        public Method SelectedMethod { get; set; }
    } 
    public enum Method
    {
        Адитивний,
        Генетичний,
        Обидва
    }
}