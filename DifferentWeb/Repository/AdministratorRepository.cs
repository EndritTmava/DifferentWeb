using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollegeApp.Models;
namespace CollegeApp.Repository
{
    public class AdministratorRepository
    {
        CollegeContext dbcontext = new CollegeContext();
        public List<Administrator> GetAdministrator()
        {
            return dbcontext.Administrators.Include("Role").ToList();

        }
        public void CreateAdministrator(Administrator obj)
        {
            dbcontext.Administrators.Add(obj);
        }
    }
}