using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollegeApp.Models;

namespace CollegeApp.Repository
{
    public class ProfessorRepository
    {
        public List<Professor> GetProfessors()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.Professors.Include("Role").ToList();
        }
    }
}