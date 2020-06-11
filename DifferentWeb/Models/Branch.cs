using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Branch
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression("[a-zA-z]")]
        [MinLength(2)]
        [MaxLength(25)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }
        public virtual Departament department { get; set; }
        [Required]
        public int DepartamentID { get; set; }
        public List<Student> Students { get; set; }
    }
}