using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DifferentWeb.Models
{
    public class Subject
    {
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public virtual Semester Semester { get; set; }
        public int SemestrID { get; set; }
        public int BranchID { get; set; }
        public virtual Departament Departament { get; set; }
        public int ProfessorID { get; set; }
        public virtual Professor Professor { get; set; }
    }
}