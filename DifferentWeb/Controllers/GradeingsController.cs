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
    public class GradeingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gradeings
        public ActionResult Index()
        {
            var gradeings = db.Gradeings.Include(g => g.Student);
            return View(gradeings.ToList());
        }

        // GET: Gradeings/Details/5
        public ActionResult Details(int? id)
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

        // Custom ActionResult
        public ActionResult ReadGrades(string id)
        {
            if (id == null || id != User.Identity.GetUserName())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gradeings = db.Gradeings.Where(x => x.Student.ID == id);
            if (gradeings == null)
            {
                return HttpNotFound();
            }
            return View(gradeings.ToList());
        }

        // GET: Gradeings/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName");
            return View();
        }

        // POST: Gradeings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StudentID,Result,GradingDate")] Gradeing gradeing)
        {
            if (ModelState.IsValid)
            {
                db.Gradeings.Add(gradeing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName", gradeing.StudentID);
            return View(gradeing);
        }

        // GET: Gradeings/Edit/5
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
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName", gradeing.StudentID);
            return View(gradeing);
        }

        // POST: Gradeings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentID,Result,GradingDate")] Gradeing gradeing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gradeing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ParentName", gradeing.StudentID);
            return View(gradeing);
        }

        // GET: Gradeings/Delete/5
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
