using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DifferentWeb.Models
{
    public class Branch
    {
        public int ID { get; set; }
        public string BranchName { get; set; }
        public virtual Departament department { get; set; }
        public int DepartamentID { get; set; }
        public List<Student> Students { get; set; }
    }
}