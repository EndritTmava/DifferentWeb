using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Person
    {

        public int ID { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        [RegularExpression("[A-Za-z]")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        [RegularExpression("[A-Za-z]")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required]

        [RegularExpression("[A-Za-z]")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression("[0-9]")]
        [Display(Name = "Personal Number")]
        public string PersonalNumber { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy hh:mm")]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(25)]
        [RegularExpression("[A-Za-z]")]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(25)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@" ^ ([0 - 9a - zA - Z]([\+\-_\.][0 - 9a - zA - Z] +) *)+@(([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]*\\.)+[a-zA-Z0-9]{2,17})$")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        [RegularExpression(@"^\+?\d.\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{3}?")]
        [Display(Name = "PhoneNo")]
        public string PhoneNo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
    }
}