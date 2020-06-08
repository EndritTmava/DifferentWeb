using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Departament
    {
        public int ID { get; set; }
        public string DepartamentName { get; set; }
        public List<Branch> Branches { get; set; }
    }
}