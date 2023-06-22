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
    public class svController : Controller
    {
        private db_psmportalEntities db = new db_psmportalEntities();

        // GET: sv
        public ActionResult Index()
        {
            var tb_sv = db.tb_sv.Include(t => t.tb_lecturer);
            return View(tb_sv.ToList());
        }

        // GET: sv/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_sv tb_sv = db.tb_sv.Find(id);
            if (tb_sv == null)
            {
                return HttpNotFound();
            }
            return View(tb_sv);
        }

        // GET: sv/Create
        public ActionResult Create()
        {
            ViewBag.SupervisorIC = new SelectList(db.tb_lecturer, "IC", "Name");
            return View();
        }

        // POST: sv/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupervisorIC,OwnedStudentIC")] tb_sv tb_sv)
        {
            if (ModelState.IsValid)
            {
                db.tb_sv.Add(tb_sv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupervisorIC = new SelectList(db.tb_lecturer, "IC", "Name", tb_sv.SupervisorIC);
            return View(tb_sv);
        }

        // GET: sv/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_sv tb_sv = db.tb_sv.Find(id);
            if (tb_sv == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupervisorIC = new SelectList(db.tb_lecturer, "IC", "Name", tb_sv.SupervisorIC);
            return View(tb_sv);
        }

        // POST: sv/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupervisorIC,OwnedStudentIC")] tb_sv tb_sv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_sv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupervisorIC = new SelectList(db.tb_lecturer, "IC", "Name", tb_sv.SupervisorIC);
            return View(tb_sv);
        }

        // GET: sv/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_sv tb_sv = db.tb_sv.Find(id);
            if (tb_sv == null)
            {
                return HttpNotFound();
            }
            return View(tb_sv);
        }

        // POST: sv/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_sv tb_sv = db.tb_sv.Find(id);
            db.tb_sv.Remove(tb_sv);
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
