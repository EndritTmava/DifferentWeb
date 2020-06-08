using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DifferentWeb.Models;

namespace DifferentWeb.Repository
{
    public class DepartamentRepository
    {
        public List<Departament> GetDepartaments()
        {
            CollegeContext context = new CollegeContext();
           return context.Departaments.ToList();
        } 
    }
}