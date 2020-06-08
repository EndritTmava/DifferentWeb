using DifferentWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DifferentWeb.Repository
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