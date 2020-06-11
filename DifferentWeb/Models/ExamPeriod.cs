using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class ExamPeriod
    {
        public int ID { get; set; }
        [Required]
        [MinLength(3)]
        [Display(Name = "Period Name")]
        public string PeriodName { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy hh:mm")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [MinLength(3)]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy hh:mm")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}