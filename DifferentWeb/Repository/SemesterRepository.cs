using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DifferentWeb.Models;

namespace DifferentWeb.Repository
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