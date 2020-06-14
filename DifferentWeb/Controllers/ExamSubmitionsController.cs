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
    public class ExamSubmitionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExamSubmitions
        public ActionResult Index()
        {
            var examSubmitions = db.ExamSubmitions.Include(e => e.ExamPeriod).Include(e => e.Student).Include(e => e.Subject);
            return View(examSubmitions.ToList());
        }

        // GET: ExamSubmitions/Details/5
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
        public ActionResult Create()
        {
            ViewBag.ExamPeriodID = new SelectList(db.ExamPeriods, "ID", "PeriodName");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName");
            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "SubjectName");
            return View();
        }

        // POST: ExamSubmitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SubjectID,StudentID,DateOfSubmition,ExamPeriodID")] ExamSubmition examSubmition)
        {
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
