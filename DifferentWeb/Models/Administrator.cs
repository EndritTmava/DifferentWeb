using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Administrator:Person
    {
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}