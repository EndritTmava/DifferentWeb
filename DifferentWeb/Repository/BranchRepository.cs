using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollegeApp.Models;
namespace CollegeApp.Repository
{
    public class BranchRepository
    {
        public List<Branch> GetBranches()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.Branches.Include("Departament").ToList();
        }
    }
}