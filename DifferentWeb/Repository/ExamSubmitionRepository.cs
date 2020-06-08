using DifferentWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DifferentWeb.Repository
{
    public class ExamSubmitionRepository
    {
        public List<ExamSubmition> GetExamSubmitions()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.ExamSubmitions.Include("ExamPeriod").ToList();
        }
    }
}