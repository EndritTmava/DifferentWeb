using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DifferentWeb.Models;
using DifferentWeb.Repository;

namespace CollegeApp.Repository
{
    public class DifferentWeb
    {
        public List<Role> GetRoles()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.Roles.ToList();
        }
    }
}