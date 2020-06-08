using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollegeApp.Models;
namespace CollegeApp.Repository
{
    public class GradeingRepository
    {

        public List<Gradeing> GetGradeings()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.Gradeings.Include("Student").Include("ExamSubmition").ToList();
        }
    }
}