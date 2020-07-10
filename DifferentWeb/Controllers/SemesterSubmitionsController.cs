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
    [Authorize(Roles = "Admin")]
    public class SemesterSubmitionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SemesterSubmitions
        public ActionResult Index()
        {
            var semesterSubmitions = db.SemesterSubmitions.Include(s => s.Student);
            return View(semesterSubmitions.ToList());
        }

        // GET: SemesterSubmitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterSubmition semesterSubmition = db.SemesterSubmitions.Find(id);
            if (semesterSubmition == null)
            {
                return HttpNotFound();
            }
            return View(semesterSubmition);
        }

        // GET: SemesterSubmitions/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName");
            return View();
        }

        // POST: SemesterSubmitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SemestrID,StudentID,SemesterRegistrationDate")] SemesterSubmition semesterSubmition)
        {
            if (ModelState.IsValid)
            {
                db.SemesterSubmitions.Add(semesterSubmition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName", semesterSubmition.StudentID);
            return View(semesterSubmition);
        }

        // GET: SemesterSubmitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterSubmition semesterSubmition = db.SemesterSubmitions.Find(id);
            if (semesterSubmition == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName", semesterSubmition.StudentID);
            return View(semesterSubmition);
        }

        // POST: SemesterSubmitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SemestrID,StudentID,SemesterRegistrationDate")] SemesterSubmition semesterSubmition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semesterSubmition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName", semesterSubmition.StudentID);
            return View(semesterSubmition);
        }

        // GET: SemesterSubmitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterSubmition semesterSubmition = db.SemesterSubmitions.Find(id);
            if (semesterSubmition == null)
            {
                return HttpNotFound();
            }
            return View(semesterSubmition);
        }

        // POST: SemesterSubmitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SemesterSubmition semesterSubmition = db.SemesterSubmitions.Find(id);
            db.SemesterSubmitions.Remove(semesterSubmition);
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
