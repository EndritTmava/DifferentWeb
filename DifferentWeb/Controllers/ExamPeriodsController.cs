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
    public class ExamPeriodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExamPeriods
        public ActionResult Index()
        {
            return View(db.ExamPeriods.ToList());
        }

        // GET: ExamPeriods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamPeriod examPeriod = db.ExamPeriods.Find(id);
            if (examPeriod == null)
            {
                return HttpNotFound();
            }
            return View(examPeriod);
        }

        // GET: ExamPeriods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExamPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PeriodName,StartDate,EndDate")] ExamPeriod examPeriod)
        {
            if (ModelState.IsValid)
            {
                db.ExamPeriods.Add(examPeriod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(examPeriod);
        }

        // GET: ExamPeriods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamPeriod examPeriod = db.ExamPeriods.Find(id);
            if (examPeriod == null)
            {
                return HttpNotFound();
            }
            return View(examPeriod);
        }

        // POST: ExamPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PeriodName,StartDate,EndDate")] ExamPeriod examPeriod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examPeriod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(examPeriod);
        }

        // GET: ExamPeriods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamPeriod examPeriod = db.ExamPeriods.Find(id);
            if (examPeriod == null)
            {
                return HttpNotFound();
            }
            return View(examPeriod);
        }

        // POST: ExamPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamPeriod examPeriod = db.ExamPeriods.Find(id);
            db.ExamPeriods.Remove(examPeriod);
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
