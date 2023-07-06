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
    public class studentController : Controller
    {
        private db_psmportalEntities1 db = new db_psmportalEntities1();

        // GET: student
        public ActionResult Index()
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "4") // supervisor
            {
                // Get the supervisor IC from the session
                string supervisorIC = Session["IC"].ToString();

                // Store the supervisor IC in ViewBag
                ViewBag.SupervisorIC = supervisorIC;

                // Retrieve the student list owned by the supervisor
                var tb_student = db.tb_student
                    .Where(s => s.tb_sv.SupervisorIC == supervisorIC)
                    .Include(t => t.tb_domain)
                    .Include(t => t.tb_program)
                    .Include(t => t.tb_proposal)
                    .Include(t => t.tb_sv)
                    .ToList();

                return View(tb_student);
            }

            // For other roles, return an empty student list
            return View(new List<tb_student>());
        }


        // GET: student/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            return View(tb_student);
        }

        // GET: student/Create
        public ActionResult Create()
        {
            ViewBag.Domain = new SelectList(db.tb_domain, "DomainID", "DomainName");
            ViewBag.ProgramCode = new SelectList(db.tb_program, "ProgramID", "ProgramName");
            ViewBag.ProposalID = new SelectList(db.tb_proposal, "ProposalID", "Title");
            ViewBag.Supervisor = new SelectList(db.tb_sv, "SupervisorIC", "OwnedStudentIC");
            return View();
        }

        // POST: student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IC,MatricNo,Name,ProgramCode,Email,MobileNo,AcademicSession,Supervisor,Agreement,Domain,ProposalID")] tb_student tb_student)
        {
            if (ModelState.IsValid)
            {
                // Create a new instance of tb_user and assign values
                tb_user user = new tb_user();
                user.IC = tb_student.IC;
                user.Password = tb_student.MobileNo;
                user.Role = 3;

                // Add the user to the context and save changes
                db.tb_user.Add(user);
                db.SaveChanges();

                // Assign the user's IC to the tb_student object
                tb_student.IC = user.IC;

                // Add the student to tb_student and save changes
                db.tb_student.Add(tb_student);
                db.SaveChanges();

                return RedirectToAction("Index", "Login");
            }

            ViewBag.Domain = new SelectList(db.tb_domain, "DomainID", "DomainName", tb_student.Domain);
            ViewBag.ProgramCode = new SelectList(db.tb_program, "ProgramID", "ProgramName", tb_student.ProgramCode);
            ViewBag.ProposalID = new SelectList(db.tb_proposal, "ProposalID", "Title", tb_student.ProposalID);
            ViewBag.Supervisor = new SelectList(db.tb_sv, "SupervisorIC", "OwnedStudentIC", tb_student.Supervisor);
            return View(tb_student);
        }


        // GET: student/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Domain = new SelectList(db.tb_domain, "DomainID", "DomainName", tb_student.Domain);
            ViewBag.ProgramCode = new SelectList(db.tb_program, "ProgramID", "ProgramName", tb_student.ProgramCode);
            ViewBag.ProposalID = new SelectList(db.tb_proposal, "ProposalID", "Title", tb_student.ProposalID);
            ViewBag.Supervisor = new SelectList(db.tb_sv, "SupervisorIC", "OwnedStudentIC", tb_student.Supervisor);
            return View(tb_student);
        }

        // POST: student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IC,MatricNo,Name,ProgramCode,Email,MobileNo,AcademicSession,Supervisor,Agreement,Domain,ProposalID")] tb_student tb_student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Domain = new SelectList(db.tb_domain, "DomainID", "DomainName", tb_student.Domain);
            ViewBag.ProgramCode = new SelectList(db.tb_program, "ProgramID", "ProgramName", tb_student.ProgramCode);
            ViewBag.ProposalID = new SelectList(db.tb_proposal, "ProposalID", "Title", tb_student.ProposalID);
            ViewBag.Supervisor = new SelectList(db.tb_sv, "SupervisorIC", "OwnedStudentIC", tb_student.Supervisor);
            return View(tb_student);
        }

        // GET: student/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            return View(tb_student);
        }

        // POST: student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_student tb_student = db.tb_student.Find(id);
            db.tb_student.Remove(tb_student);
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
