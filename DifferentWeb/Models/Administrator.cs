using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Administrator:Person
    {
        [Required]
        public int RoleID { get; set; }
        public Role Role { get; set; }

    }

}