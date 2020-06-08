using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeApp.Models
{
    public class Student:Person
    {
        public string ParentName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentEmail { get; set; }
        public string ParentPhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual int BranchID { get; set; }
        public virtual Branch Branch { get; set; }
        public int FirstSemesterID { get; set; }
        public virtual Semester Semester { get; set; }
        public Role Role { get; set; }
        public int SemesterID { get; set; }
        public List<Gradeing> Grades { get; set; }
    }
}