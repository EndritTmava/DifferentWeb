using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollegeApp.Models;

namespace CollegeApp.Repository
{
    public class RoleRepository
    {
        public List<Role> GetRoles()
        {
            CollegeContext dbcontext = new CollegeContext();
            return dbcontext.Roles.ToList();
        }
    }
}