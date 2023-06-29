using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using psmportal.Models;

namespace psmportal.Controllers
{
    public class lecturerController : Controller
    {
        private db_psmportalEntities db = new db_psmportalEntities();

        // GET: lecturer
        public ActionResult Index()
        {
            var tb_lecturer = db.tb_lecturer.Include(t => t.tb_domain).Include(t => t.tb_program).Include(t => t.tb_sv);
            return View(tb_lecturer.ToList());
        }

        // GET: lecturer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_lecturer tb_lecturer = db.tb_lecturer.Find(id);
            if (tb_lecturer == null)
            {
                return HttpNotFound();
            }
            return View(tb_lecturer);
        }

        // GET: lecturer/Create
        public ActionResult Create()
        {
            ViewBag.Domain = new SelectList(db.tb_domain, "DomainID", "DomainName");
            ViewBag.ProgramCode = new SelectList(db.tb_program, "ProgramID", "ProgramName");
            ViewBag.IC = new SelectList(db.tb_sv, "SupervisorIC", "OwnedStudentIC");
            return View();
        }

        // POST: lecturer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IC,Name,ProgramCode,Domain,Email,MobileNo")] tb_lecturer tb_lecturer, string MobileNo, bool isCommittee)
        {
            if (ModelState.IsValid)
            {
                // Insert the user information in tb_user table
                var user = new tb_user
                {
                    IC = tb_lecturer.IC,
                    Password = MobileNo,
                    Role = isCommittee ? 2 : 4 // Set role based on the value of isCommittee
                };
                db.tb_user.Add(user);
                db.SaveChanges();

                // Insert the lecturer information in tb_lecturer table
                db.tb_lecturer.Add(tb_lecturer);
                db.SaveChanges();

                if (isCommittee)
                {
                    // Insert the committee member information in tb_committee table
                    var committee = new tb_committee
                    {
                        IC = tb_lecturer.IC
                    };
                    db.tb_committee.Add(committee);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            // If there are validation errors, debug and inspect the ModelState.Values collection
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    // Examine the error and related properties to identify the specific validation issue
                    var errorMessage = error.ErrorMessage;
                }
            }

            ViewBag.Domain = new SelectList(db.tb_domain, "DomainID", "DomainName", tb_lecturer.Domain);
            ViewBag.ProgramCode = new SelectList(db.tb_program, "ProgramID", "ProgramName", tb_lecturer.ProgramCode);
            ViewBag.IC = new SelectList(db.tb_sv, "SupervisorIC", "OwnedStudentIC", tb_lecturer.IC);
            return View(tb_lecturer);
        }



        // GET: lecturer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_lecturer tb_lecturer = db.tb_lecturer.Find(id);
            if (tb_lecturer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Domain = new SelectList(db.tb_domain, "DomainID", "DomainName", tb_lecturer.Domain);
            ViewBag.ProgramCode = new SelectList(db.tb_program, "ProgramID", "ProgramName", tb_lecturer.ProgramCode);
            ViewBag.IC = new SelectList(db.tb_sv, "SupervisorIC", "OwnedStudentIC", tb_lecturer.IC);
            return View(tb_lecturer);
        }

        // POST: lecturer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IC,Name,ProgramCode,Domain,Email,MobileNo")] tb_lecturer tb_lecturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_lecturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Domain = new SelectList(db.tb_domain, "DomainID", "DomainName", tb_lecturer.Domain);
            ViewBag.ProgramCode = new SelectList(db.tb_program, "ProgramID", "ProgramName", tb_lecturer.ProgramCode);
            ViewBag.IC = new SelectList(db.tb_sv, "SupervisorIC", "OwnedStudentIC", tb_lecturer.IC);
            return View(tb_lecturer);
        }

        // GET: lecturer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_lecturer tb_lecturer = db.tb_lecturer.Find(id);
            if (tb_lecturer == null)
            {
                return HttpNotFound();
            }
            return View(tb_lecturer);
        }

        // POST: lecturer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_lecturer tb_lecturer = db.tb_lecturer.Find(id);

            // Remove the lecturer from tb_lecturer table
            db.tb_lecturer.Remove(tb_lecturer);

            // Find and remove the corresponding user from tb_user table
            tb_user user = db.tb_user.Find(id);
            db.tb_user.Remove(user);

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
