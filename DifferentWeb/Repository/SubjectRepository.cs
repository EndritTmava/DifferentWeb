using CollegeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeApp.Repository
{
    public class SubjectRepository
    {
        public List<Subject> GetSubjects()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.Subjects.Include("Semester").Include("Branch").Include("Professor").ToList();
        }
    }
}