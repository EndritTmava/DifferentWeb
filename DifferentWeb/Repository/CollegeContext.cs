using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DifferentWeb.Models;

namespace DifferentWeb.Repository
{
    public class CollegeContext:DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<ExamPeriod> ExamPeriods { get; set; }
        public DbSet<ExamSubmition> ExamSubmitions { get; set; }
        public DbSet<Gradeing> Gradeings { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SemesterSubmition> SemesterSubmitions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}