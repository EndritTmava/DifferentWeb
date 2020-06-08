using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class ExamSubmition
    {
        public int ID { get; set; }
        public int SubjectID { get; set; }
        public virtual Subject  Subject { get; set; }
        public virtual Student Student { get; set; }
        public int StudentID { get; set; }
        public DateTime DateOfSubmition { get; set; }
        public ExamPeriod ExamPeriod { get; set; }
        public int ExamPeriodID { get; set; }
    }
}