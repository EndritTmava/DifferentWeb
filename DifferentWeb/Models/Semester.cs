using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Semester
    {

        public int ID { get; set; }
        [Display(Name = "Semester")]
        public string semester { get; set; }
    }
}