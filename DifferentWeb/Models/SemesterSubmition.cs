using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeApp.Models
{
    public class SemesterSubmition
    {
        public int ID { get; set; }
        public int SemestrID { get; set; }
        public virtual Semester Semester { get; set; }
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
        public DateTime SemesterRegistrationDate { get; set; }
    }
}