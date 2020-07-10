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

    public class GradeingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gradeings
        [Authorize(Roles = "Professor, Student")]
        public ActionResult Index()
        {


            string loggedid = User.Identity.Name;
            List<Gradeing> gradeings = db.Gradeings.Include(g => g.Subject).Where(x => x.Subject.Professor.UserId == loggedid).ToList();
            List<Subject> subjects = new List<Subject>();

            if (User.IsInRole("Professor"))
            {
                return View(gradeings.Where(x => x.Subject.Professor.UserId == User.Identity.Name).ToList());
            }
            else if (User.IsInRole("Student"))
            {

                Student student = db.Students.Where(s => s.UserId == loggedid).Include(s => s.Branch).Include(s => s.Semester).First();
                subjects = db.Subjects.Where(s => s.BranchID == student.BranchID && s.Semester.ID <= student.Semester.ID).Include(s => s.Branch).Include(s => s.Professor).Include(s => s.Semester).ToList();
                List<Gradeing> grades = db.Gradeings.Where(g => g.StudentID == loggedid).Include(g=> g.Student).Include(g => g.Subject).Include(g=> g.ExamSubmition).ToList();
                ViewBag.Grades = grades;
                return View("StudentGrades", subjects);
            }

            return View(gradeings.ToList());
        }

        // GET: Gradeings/Details/5
        [Authorize(Roles = "Professor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string loggedid = User.Identity.Name;
            List<Gradeing> gradeings = db.Gradeings.Include(g => g.Subject).Where(x => x.Subject.Professor.UserId == loggedid).ToList();

            foreach (var item in gradeings)
            {
                if (item.ID == id)
                {
                    return View(item);
                }
            }


                return HttpNotFound();

           
        }

        // GET: Gradeings/Create
        [Authorize(Roles = "Professor")]
        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.Subjects.Where(x => x.Professor.UserId == User.Identity.Name), "ID", "SubjectName");
            return View();
        }
        // POST: Gradeings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Professor")]
        public ActionResult Create([Bind(Include = "ID,StudentID,Result,GradingDate,SubjectID")] Gradeing gradeing)
        {
            List<Gradeing> grades = db.Gradeings.Where(g => g.StudentID == gradeing.StudentID).Include(g => g.Student).Include(g => g.Subject).Include(g => g.ExamSubmition).ToList();

            bool i =  db.Gradeings.Any(cus => cus.StudentID == gradeing.StudentID && cus.SubjectID == gradeing.SubjectID);


            if (ModelState.IsValid && i == false )
            {
                db.Gradeings.Add(gradeing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "SubjectName", gradeing.SubjectID);
            return View(gradeing);
        }

        // GET: Gradeings/Edit/5
        [Authorize(Roles = "Professor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gradeing gradeing = db.Gradeings.Find(id);
            if (gradeing == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "SubjectName", gradeing.SubjectID);
            return View(gradeing);
        }


        // POST: Gradeings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Professor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentID,Result,GradingDate,SubjectID")] Gradeing gradeing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gradeing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "SubjectName", gradeing.SubjectID);
            return View(gradeing);
        }

        // GET: Gradeings/Delete/5
        [Authorize(Roles = "Professor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gradeing gradeing = db.Gradeings.Find(id);
            if (gradeing == null)
            {
                return HttpNotFound();
            }
            return View(gradeing);
        }

        // POST: Gradeings/Delete/5
        [Authorize(Roles = "Professor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gradeing gradeing = db.Gradeings.Find(id);
            db.Gradeings.Remove(gradeing);
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
