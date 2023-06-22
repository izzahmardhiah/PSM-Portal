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
        private db_psmportalEntities db = new db_psmportalEntities();

        // GET: student
        public ActionResult Index()
        {
            var tb_student = db.tb_student.Include(t => t.tb_domain).Include(t => t.tb_program).Include(t => t.tb_proposal).Include(t => t.tb_sv);
            return View(tb_student.ToList());
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
                db.tb_student.Add(tb_student);
                db.SaveChanges();
                return RedirectToAction("Index");
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
