using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace DifferentWeb.Models
{
    public class Subject
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Subject Name")]
        [MinLength(3)]
        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
        public virtual Semester Semester { get; set; }
        [Required(ErrorMessage ="Pick Semester")]
        [RegularExpression("[0-9]")]
        public int SemesterID { get; set; }
        [Required(ErrorMessage = "Pick Branch")]
        [RegularExpression("[0-9]")]
        public int BranchID { get; set; }
        public virtual Branch Branch { get; set; }
        [Required(ErrorMessage = "Pick Professor")]
        [RegularExpression("[0-9]")]
        public int ProfessorID { get; set; }
        public virtual Professor Professor { get; set; }
        [NotMapped]
        public double AverageGrade { get; set; }

        [NotMapped]
        public int Grade { get; set; }
    }
}