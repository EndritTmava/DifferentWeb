using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Student:Person
    {
        [Required(ErrorMessage = "Enter Parent Name")]
        [MinLength(2, ErrorMessage = "Parent name should have at least 2 characters")]
        [MaxLength(25, ErrorMessage="Parent name should have less than 25 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabet")]
        [Display(Name = "Parent Name")]
        public string ParentName { get; set; }
        [Required(ErrorMessage = "Enter parent last Name")]
        [MinLength(2, ErrorMessage = "Parent last name should have at least 2 characters")]
        [MaxLength(25, ErrorMessage = "Parent last name should have less than 25 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Parent last name is not in the correct format, Parent last Name should have only letters")]
        [Display(Name = "Parent Last Name")]
        public string ParentLastName { get; set; }
        [Required(ErrorMessage = "Enter parent email")]
        [MinLength(5, ErrorMessage = "Parent email should have at least 2 characters")]
        [MaxLength(25, ErrorMessage = "Parent email should have less than 25 characters")]
        [EmailAddress]
        [Display(Name = "Parent Email")]
        public string ParentEmail { get; set; }
        [Required(ErrorMessage = "Enter parent Password")]
        [MinLength(6, ErrorMessage = "Password should have at least 6 characters")]
        [MaxLength(25, ErrorMessage = "Password should have less than 25 characters")]
       // [RegularExpression(@"^\+?\d.\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{3}?", ErrorMessage = "Phone number is not in the correct format")]
        [Display(Name = "Parent phone number")]
        public string ParentPhoneNumber { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy hh:mm")]
        public DateTime RegistrationDate { get; set; }
      //  [Required( ErrorMessage = "Please pick brach")]
        public virtual int BranchID { get; set; }
        public virtual Branch Branch { get; set; }



      //  [Required]
        public int FirstSemesterID { get; set; }
        public virtual Semester Semester { get; set; }
    //    [Required]
        public int SemesterID { get; set; }
        public List<Gradeing> Grades { get; set; }

    }
}