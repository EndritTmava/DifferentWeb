using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeApp.Models
{
    public class Professor:Person
    {
        public string Qualification { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}