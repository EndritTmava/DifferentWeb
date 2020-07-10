using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DifferentWeb.Models;






namespace DifferentWeb.Controllers
{

    public class SubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subjects
        [Authorize(Roles = "Admin,Student")]
        public ActionResult Index()
        {

            string loggedid = User.Identity.Name;
            List<Subject> subjects = new List<Subject>();
            if (User.IsInRole("Admin"))
            {
                subjects = db.Subjects.Include(s => s.Branch).Include(s => s.Professor).Include(s => s.Semester).ToList(); 
            }
            else if (User.IsInRole("Student"))
            {
                Student student = db.Students.Where(s => s.UserId == loggedid).Include(s => s.Branch).Include(s => s.Semester).First();
                subjects = db.Subjects.Where(s=> s.BranchID == student.BranchID && s.Semester.ID<= student.Semester.ID).Include(s => s.Branch).Include(s => s.Professor).Include(s => s.Semester).ToList();
                return View("StudentSubjects", subjects);
            }

            return View(subjects.ToList());
        }

        // GET: Subjects/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Subject subject = db.Subjects.Find(id);
            List<Gradeing> gradeings = db.Gradeings.Include(g => g.Student).Include(g => g.Subject).ToList();


            var averageGrades =

            from averageGrade
                    in gradeings
            group averageGrade by averageGrade.Subject.ID;
            ;

            foreach (var item in averageGrades)
            {

                double d = item.Average(x => x.Result);

                if (item.Key == subject.ID)
                {
                    subject.AverageGrade = d;
                }


            }


            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName");
            ViewBag.ProfessorID = new SelectList(db.Professors, "ID", "Qualification");
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SubjectName,SemesterID,BranchID,ProfessorID")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName", subject.BranchID);
            ViewBag.ProfessorID = new SelectList(db.Professors, "ID", "Qualification", subject.ProfessorID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester", subject.SemesterID);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName", subject.BranchID);
            ViewBag.ProfessorID = new SelectList(db.Professors, "ID", "Qualification", subject.ProfessorID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester", subject.SemesterID);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SubjectName,SemesterID,BranchID,ProfessorID")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName", subject.BranchID);
            ViewBag.ProfessorID = new SelectList(db.Professors, "ID", "Qualification", subject.ProfessorID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester", subject.SemesterID);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
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

