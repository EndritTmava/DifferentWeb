using CollegeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeApp.Repository
{
    public class ExamPeriodRepository
    {
        public List<ExamPeriod> GetExamPeriods()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.ExamPeriods.ToList();
        }
    }
}