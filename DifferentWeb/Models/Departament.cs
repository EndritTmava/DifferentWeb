using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Departament
    {


        public int ID { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        [Display(Name = "Departament Name")]
        public string DepartamentName { get; set; }
        public List<Branch> Branches { get; set; }
    }
}