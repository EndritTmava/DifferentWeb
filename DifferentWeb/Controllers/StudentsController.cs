using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DifferentWeb.Models;
using Microsoft.AspNet.Identity.Owin;

namespace DifferentWeb.Controllers
{
    public class StudentsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();




        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public StudentsController()
        {
        }

        public StudentsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Branch).Include(s => s.Semester);
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
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "ID,ParentName,ParentLastName,ParentEmail,ParentPhoneNumber,RegistrationDate,BranchID,FirstSemesterID,SemesterID,UserId,Name,LastName,Gender,PersonalNumber,Birthday,Country,City,Email,PhoneNo,Password")] Student student)
        {

            string lastid = db.Students.Max(p => p.UserId);

            if (lastid != null)
            {

                lastid = lastid.Substring(0, lastid.Length - 1);
                int id = int.Parse(lastid);
                student.UserId = $"{id + 1}S";
            }

            else
            {
                student.UserId = $"1S";//"{id+1}A";
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = student.UserId, Email = student.Email };
                var result = await UserManager.CreateAsync(user, student.Password);
                string uid = user.Id;

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    db.Students.Add(student);

                    await UserManager.AddToRoleAsync(uid, "Student");
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName", student.BranchID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester", student.SemesterID);
            return View(student);
        }

        // GET: Students

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
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester", student.SemesterID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ParentName,ParentLastName,ParentEmail,ParentPhoneNumber,RegistrationDate,BranchID,FirstSemesterID,SemesterID,UserId,Name,LastName,Gender,PersonalNumber,Birthday,Country,City,Email,PhoneNo,Password")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName", student.BranchID);
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
