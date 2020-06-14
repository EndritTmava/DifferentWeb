using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DifferentWeb.Models;
using DifferentWeb.Repository;
using Microsoft.AspNet.Identity;

namespace DifferentWeb.Controllers
{
    public class StudentsController : Controller
    {
        private CollegeContext db = new CollegeContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Branch).Include(s => s.Role).Include(s => s.Semester);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //Custom ActionResult
        public ActionResult Profile(string id)
        {
            if (id != User.Identity.GetUserName())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(User.Identity.GetUserName());
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName");
            ViewBag.RoleId = new SelectList(db.Roles, "ID", "role");
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ParentName,ParentLastName,ParentEmail,ParentPhoneNumber,RegistrationDate,BranchID,FirstSemesterID,SemesterID,RoleId,Name,LastName,Gender,PersonalNumber,Birthday,Country,City,Email,PhoneNo,Password")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName", student.BranchID);
            ViewBag.RoleId = new SelectList(db.Roles, "ID", "role", student.RoleId);
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester", student.SemesterID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName", student.BranchID);
            ViewBag.RoleId = new SelectList(db.Roles, "ID", "role", student.RoleId);
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester", student.SemesterID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ParentName,ParentLastName,ParentEmail,ParentPhoneNumber,RegistrationDate,BranchID,FirstSemesterID,SemesterID,RoleId,Name,LastName,Gender,PersonalNumber,Birthday,Country,City,Email,PhoneNo,Password")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName", student.BranchID);
            ViewBag.RoleId = new SelectList(db.Roles, "ID", "role", student.RoleId);
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester", student.SemesterID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
