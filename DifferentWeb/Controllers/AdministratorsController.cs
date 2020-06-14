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
    public class AdministratorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();




        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdministratorsController()
        {
        }

        public AdministratorsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "ID,Name,LastName,Gender,PersonalNumber,Birthday,Country,City,Email,PhoneNo,Password")] Administrator administrator)
        {


            string lastid = db.Administrators.Max(p => p.UserId);
            if (lastid != null)
            {

                lastid = lastid.Substring(0, lastid.Length - 1);
                int id = int.Parse(lastid);
                administrator.UserId = $"{id + 1}A";
            }
            else
            {
                administrator.UserId = $"1A";//"{id+1}A";
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = administrator.UserId, Email = administrator.Email };
                var result = await UserManager.CreateAsync(user, administrator.Password);
                string uid = user.Id;

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    db.Administrators.Add(administrator);

                    await UserManager.AddToRoleAsync(uid, "Admin");
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(administrator);






        }

        // GET: Administrators
        public ActionResult Index()
        {
            var administrators = db.Administrators;//.Include(a => a.Role);
            return View(administrators.ToList());
        }

        // GET: Administrators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // GET: Administrators/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "ID", "role");
            return View();
        }

        // POST: Administrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      

        // GET: Administrators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
           // ViewBag.RoleID = new SelectList(db.Roles, "ID", "role", administrator.RoleID);
            return View(administrator);
        }

        // POST: Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoleID,Name,LastName,Gender,PersonalNumber,Birthday,Country,City,Email,PhoneNo,Password")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          //  ViewBag.RoleID = new SelectList(db.Roles, "ID", "role", administrator.RoleID);
            return View(administrator);
        }

        // GET: Administrators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrator administrator = db.Administrators.Find(id);
            db.Administrators.Remove(administrator);
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
