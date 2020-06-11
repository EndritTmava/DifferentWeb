using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [RegularExpression("[A-Za-z]")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
        public virtual Semester Semester { get; set; }
        [Required(ErrorMessage ="Pick Semester")]
        [RegularExpression("[0-9]")]
        public int SemestrID { get; set; }
        [Required(ErrorMessage = "Pick Branch")]
        [RegularExpression("[0-9]")]
        public int BranchID { get; set; }
        public virtual Departament Departament { get; set; }
        [Required(ErrorMessage = "Pick Professor")]
        [RegularExpression("[0-9]")]
        public int ProfessorID { get; set; }
        public virtual Professor Professor { get; set; }
    }
}