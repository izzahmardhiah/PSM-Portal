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
    public class committeeController : Controller
    {
        private db_psmportalEntities db = new db_psmportalEntities();

        // GET: committee
        public ActionResult Index()
        {
            var tb_committee = db.tb_committee.Include(t => t.tb_lecturer).Include(t => t.tb_lecturer);
            return View(tb_committee.ToList());
        }

        // GET: committee/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_committee tb_committee = db.tb_committee.Find(id);
            if (tb_committee == null)
            {
                return HttpNotFound();
            }
            return View(tb_committee);
        }

        // GET: committee/Create
        public ActionResult Create()
        {
            ViewBag.IC = new SelectList(db.tb_lecturer, "IC", "Name");
            ViewBag.IC = new SelectList(db.tb_lecturer, "IC", "Name");
            return View();
        }

        // POST: committee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IC")] tb_committee tb_committee)
        {
            if (ModelState.IsValid)
            {
                db.tb_committee.Add(tb_committee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IC = new SelectList(db.tb_lecturer, "IC", "Name", tb_committee.IC);
            ViewBag.IC = new SelectList(db.tb_lecturer, "IC", "Name", tb_committee.IC);
            return View(tb_committee);
        }

        // GET: committee/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_committee tb_committee = db.tb_committee.Find(id);
            if (tb_committee == null)
            {
                return HttpNotFound();
            }
            ViewBag.IC = new SelectList(db.tb_lecturer, "IC", "Name", tb_committee.IC);
            ViewBag.IC = new SelectList(db.tb_lecturer, "IC", "Name", tb_committee.IC);
            return View(tb_committee);
        }

        // POST: committee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IC")] tb_committee tb_committee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_committee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IC = new SelectList(db.tb_lecturer, "IC", "Name", tb_committee.IC);
            ViewBag.IC = new SelectList(db.tb_lecturer, "IC", "Name", tb_committee.IC);
            return View(tb_committee);
        }

        // GET: committee/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_committee tb_committee = db.tb_committee.Find(id);
            if (tb_committee == null)
            {
                return HttpNotFound();
            }
            return View(tb_committee);
        }

        // POST: committee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_committee tb_committee = db.tb_committee.Find(id);
            db.tb_committee.Remove(tb_committee);
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
