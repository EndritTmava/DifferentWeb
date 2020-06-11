using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Role
    {

        public int ID { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        [RegularExpression("[a-zA-Z]")]
        [Display(Name = "Role")]
        public string role { get; set; }
    }  
}