using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DifferentWeb.Models;

namespace CollegeApp.Repository
{
    public class DifferentWeb.Repository
    {
        public List<Role> GetRoles()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.Roles.ToList();
        }
    }
}