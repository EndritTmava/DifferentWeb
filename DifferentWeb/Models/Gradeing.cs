using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Gradeing
    {
        public int ID { get; set;}
        public Student Student { get; set; } 
        public int StudentID { get; set; }
        public double Result { get; set; }
        public DateTime GradingDate { get; set; }
        public Subject Subject { get; set; }
        public ExamSubmition ExamSubmition { get; set; }
    }
}