using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Gradeing
    {
        public int ID { get; set;}
        public Student Student { get; set; }
        [Required]
        [Display(Name = "Student Id")]
        public int StudentID { get; set; }
        [Required]
        [RegularExpression("[0-9]")]
        [Display(Name = "Result")]
        public double Result { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy hh:mm")]
        [Display(Name = "Grading Date")]
        public DateTime GradingDate { get; set; }
        public Subject Subject { get; set; }
        public ExamSubmition ExamSubmition { get; set; }
    }
}