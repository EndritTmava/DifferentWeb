using CollegeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeApp.Repository
{
    public class SemesterSubmitionRepository
    {
        public List<SemesterSubmition> GetSemesterSubmittions()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.SemesterSubmitions.Include("Semester").Include("Student").ToList();
        }
    }
}