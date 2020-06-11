using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class SemesterSubmition
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression("[0-9]")]
        public int SemestrID { get; set; }
        public virtual Semester Semester { get; set; }
        [Required]
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy hh:mm")]
        public DateTime SemesterRegistrationDate { get; set; }
    }
}