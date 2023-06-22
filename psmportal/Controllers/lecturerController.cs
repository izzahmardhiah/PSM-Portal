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
        public ActionResult Create([Bind(Include = "IC,Name,ProgramCode,Domain,Email,MobileNo")] tb_lecturer tb_lecturer)
        {
            if (ModelState.IsValid)
            {
                db.tb_lecturer.Add(tb_lecturer);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            db.tb_lecturer.Remove(tb_lecturer);
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
