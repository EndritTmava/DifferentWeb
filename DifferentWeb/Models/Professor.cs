using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Professor:Person
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }
        [Required]
        public int RoleID { get; set; }

        public Role Role { get; set; }
    }
}