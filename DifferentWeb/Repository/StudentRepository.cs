using DifferentWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DifferentWeb.Repository
{
    public class StudentRepository
    {
        public List<Student> GetStudents()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.Students.Include("Branch").Include("Semester").Include("Role").ToList();
        }
    }
}