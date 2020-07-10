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

    public class ProfessorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        static int userId = 5;


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ProfessorsController()
        {
        }

        public ProfessorsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "ID,Qualification,UserId,Name,LastName,Gender,PersonalNumber,Birthday,Country,City,Email,PhoneNo,Password")] Professor professor)
        {
            string lastid = db.Professors.Max(p => p.UserId);
            if (lastid != null)
            {

                lastid = lastid.Substring(0, lastid.Length - 1);
                int id = int.Parse(lastid);
                professor.UserId = $"{id + 1}P";

            }
            else
            {
                professor.UserId = $"1P";
            }


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = professor.UserId, Email = professor.Email };
                var result = await UserManager.CreateAsync(user, professor.Password);
                string uid = user.Id;
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    db.Professors.Add(professor);


                    await UserManager.AddToRoleAsync(uid, "Professor");
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

            }

            return View(professor);











        }
        // GET: Professors
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Professors.ToList());
        }

        // GET: Professors/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: Professors/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }


        // GET: Professors/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Qualification,UserId,Name,LastName,Gender,PersonalNumber,Birthday,Country,City,Email,PhoneNo,Password")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professor);
        }

        // GET: Professors/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Professor professor = db.Professors.Find(id);
            db.Professors.Remove(professor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Professor")]
        public ActionResult MySubjects()
        {

            string loggedid = User.Identity.Name;
            List<Subject> sub = db.Subjects.Where(x => x.Professor.UserId == loggedid).ToList();

            return View(sub);
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
