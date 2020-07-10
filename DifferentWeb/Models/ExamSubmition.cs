using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class ExamSubmition
    {
        public int ID { get; set; }
        [Required]
        public int SubjectID { get; set; }

        public virtual Subject  Subject { get; set; }
        public virtual Student Student { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        [Display(Name = "Date of submition")]
        public DateTime DateOfSubmition { get; set; }

        public ExamPeriod ExamPeriod { get; set; }
        [Required]
        public int ExamPeriodID { get; set; }
    }
}