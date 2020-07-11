using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        [Authorize(Roles = "Admin,Professor")]
        [HttpPost]
        public ActionResult Index(string searchText)
        {
            List<Student> students = new List<Student>();
            if (User.IsInRole("Admin"))
            {


                foreach (var item in db.Students.ToList())
                {
                    if ($"{item.Name} {item.LastName}" == searchText|| searchText == string.Empty || item.UserId == searchText || item.Name == searchText || item.LastName == searchText)
                    {
                        students.Add(item);
                    }
                }

                return View("Index", students);
            }
            else if (User.IsInRole("Professor"))
            {

                var Astudents = (from c in db.Students
                                 join ct in db.Subjects on c.BranchID equals ct.BranchID
                                 where (ct.Professor.UserId == User.Identity.Name) && (ct.SemesterID == c.SemesterID)
                                 select new { c.UserId, c.Name, c.LastName, c.Branch.BranchName, c.Semester.semester, c.RegistrationDate, c.FirstSemesterID, c.Gender, c.Birthday, c.Country, c.City, c.Email, c.PhoneNo }).ToList();

                students = new List<Student>();

                foreach (var item in Astudents)
                {

                    if ($"{item.Name} {item.LastName}" == searchText || searchText == string.Empty || item.UserId == searchText || item.Name == searchText || item.LastName == searchText)
                    {
                        students.Add(new Student
                        {
                            UserId = item.UserId,
                            Name = item.Name,
                            LastName = item.LastName,
                            Branch = new Branch() { BranchName = item.BranchName },
                            Semester = new Semester() { semester = item.semester },
                            RegistrationDate = item.RegistrationDate,
                            FirstSemesterID = item.FirstSemesterID,
                            Gender = item.Gender,
                            Birthday = item.Birthday,
                            Country = item.Country,
                            City = item.City,
                            Email = item.Email,
                            PhoneNo = item.PhoneNo
                        });

                    }
                    
                }


                //= db.Students.Where(x => x.SemesterID == ).Include(s => s.Branch).Include(s => s.Semester).ToList();
                return View("PIndex", students);
            }
            return HttpNotFound();
        }





        [Authorize(Roles = "Admin,Professor")]
        [HttpGet]
        public ActionResult Index()
        {

            List<Student> students;
            string loggedid = User.Identity.Name;
            List<Subject> sub = db.Subjects.Where(x => x.Professor.UserId == loggedid).Include(x => x.Professor)
                .Include(x => x.Branch).Include(x => x.Semester).ToList();



            if (User.IsInRole("Admin"))
            {
                 students = db.Students.Include(s => s.Branch).Include(s => s.Semester).ToList();
                return View(students.ToList());
            }
            else if (User.IsInRole("Professor"))
            {

                var Astudents = (from c in db.Students
                                         join ct in db.Subjects on c.BranchID equals ct.BranchID
                                         where (ct.Professor.UserId == loggedid) && (ct.SemesterID == c.SemesterID)
                                         select new { c.UserId,c.Name,c.LastName,c.Branch.BranchName,c.Semester.semester,c.RegistrationDate,c.FirstSemesterID,c.Gender,c.Birthday,c.Country,c.City,c.Email,c.PhoneNo }).ToList();

                students = new List<Student>();
                foreach (var item in Astudents)
                {

                    students.Add(new Student
                    {
                        UserId = item.UserId,
                        Name = item.Name,
                        LastName = item.LastName,
                        Branch = new Branch() { BranchName = item.BranchName },
                        Semester = new Semester() { semester = item.semester },
                        RegistrationDate = item.RegistrationDate,
                        FirstSemesterID = item.FirstSemesterID,
                        Gender = item.Gender,
                        Birthday = item.Birthday,
                        Country = item.Country,
                        City = item.City,
                        Email = item.Email,
                        PhoneNo = item.PhoneNo
                    });
                }
                
                                    
                //= db.Students.Where(x => x.SemesterID == ).Include(s => s.Branch).Include(s => s.Semester).ToList();
                return View("PIndex", students);
            }

            return View();
        }

        // GET: Students/Details/5
        [Authorize(Roles = "Student")]
        public ActionResult StudentDetails()
        {


                Student student = db.Students.Where(s=> s.UserId == User.Identity.Name).First();

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }


        // GET: Students/Details/5
        [Authorize(Roles = "Admin")]
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



        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "BranchName");
            ViewBag.SemesterID = new SelectList(db.Semesters, "ID", "semester");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
