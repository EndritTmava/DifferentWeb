using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DifferentWeb.Models;
using Microsoft.AspNet.Identity;


namespace DifferentWeb.Controllers
{

    public class ExamSubmitionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExamSubmitions
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var examSubmitions = db.ExamSubmitions.Include(e => e.ExamPeriod).Include(e => e.Student).Include(e => e.Subject);
            return View(examSubmitions.ToList());
        }

        // GET: ExamSubmitions/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSubmition examSubmition = db.ExamSubmitions.Find(id);
            if (examSubmition == null)
            {
                return HttpNotFound();
            }
            return View(examSubmition);
        }

        // GET: ExamSubmitions/Create
        [Authorize(Roles = "Student")]
        public ActionResult Create()
        {
            ViewBag.ExamPeriodID = new SelectList(db.ExamPeriods.Where(x=> x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now), "ID", "PeriodName");
            List<Subject> subjects; 
            Student student = db.Students.Where(s => s.UserId == User.Identity.Name).Include(s => s.Branch).Include(s => s.Semester).First();
            subjects = db.Subjects.Where(s => s.BranchID == student.BranchID && s.Semester.ID <= student.Semester.ID ).Include(s => s.Branch).Include(s => s.Professor).Include(s => s.Semester).ToList();
            List<Gradeing> grades=  db.Gradeings.Where(g => g.StudentID == User.Identity.Name).Include(s=> s.Student).ToList();
            List<Subject> finalsub = new List<Subject>();
            foreach (var item in subjects)
            {
                if (grades.Any(i =>  i.SubjectID == item.ID)==false)
                {
                    finalsub.Add(item);
                }
            }
            ViewBag.SubjectID = new SelectList(finalsub, "ID", "SubjectName");
            return View();

        }


        // POST: ExamSubmitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SubjectID,StudentID,DateOfSubmition,ExamPeriodID")] ExamSubmition examSubmition)
        {

            string loggedid = User.Identity.Name;
            var students = db.Students.Include(s => s.Branch).Include(s => s.Semester);

            foreach (var item in students)
            {
                if (item.UserId == loggedid)
                {
                    examSubmition.StudentID = item.ID;
                }
            }


            if (ModelState.IsValid)
            {
                db.ExamSubmitions.Add(examSubmition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamPeriodID = new SelectList(db.ExamPeriods, "ID", "PeriodName", examSubmition.ExamPeriodID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName", examSubmition.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "SubjectName", examSubmition.SubjectID);
            return View(examSubmition);
        }

        // GET: ExamSubmitions/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSubmition examSubmition = db.ExamSubmitions.Find(id);
            if (examSubmition == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamPeriodID = new SelectList(db.ExamPeriods, "ID", "PeriodName", examSubmition.ExamPeriodID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName", examSubmition.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "SubjectName", examSubmition.SubjectID);
            return View(examSubmition);
        }

        // POST: ExamSubmitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,SubjectID,StudentID,DateOfSubmition,ExamPeriodID")] ExamSubmition examSubmition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examSubmition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamPeriodID = new SelectList(db.ExamPeriods, "ID", "PeriodName", examSubmition.ExamPeriodID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName", examSubmition.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "SubjectName", examSubmition.SubjectID);
            return View(examSubmition);
        }

        // GET: ExamSubmitions/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSubmition examSubmition = db.ExamSubmitions.Find(id);
            if (examSubmition == null)
            {
                return HttpNotFound();
            }
            return View(examSubmition);
        }

        // POST: ExamSubmitions/Delete/5

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamSubmition examSubmition = db.ExamSubmitions.Find(id);
            db.ExamSubmitions.Remove(examSubmition);
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
