using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollegeApp.Models;

namespace CollegeApp.Repository
{
    public class SemesterRepository
    {
        public List<Semester> GetSemesters()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.Semesters.ToList();
        }
    }
}