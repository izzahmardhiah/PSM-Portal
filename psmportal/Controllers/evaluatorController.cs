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
    public class evaluatorController : Controller
    {
        private db_psmportalEntities db = new db_psmportalEntities();

        // GET: evaluator
        public ActionResult Index()
        {
            var tb_evaluator = db.tb_evaluator.Include(t => t.tb_lecturer);
            return View(tb_evaluator.ToList());
        }

        // GET: evaluator/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_evaluator tb_evaluator = db.tb_evaluator.Find(id);
            if (tb_evaluator == null)
            {
                return HttpNotFound();
            }
            return View(tb_evaluator);
        }

        // GET: evaluator/Create
        public ActionResult Create()
        {
            ViewBag.EvaluatorIC = new SelectList(db.tb_lecturer, "IC", "Name");
            return View();
        }

        // POST: evaluator/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvaluatorIC,OwnedStudentIC")] tb_evaluator tb_evaluator)
        {
            if (ModelState.IsValid)
            {
                db.tb_evaluator.Add(tb_evaluator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EvaluatorIC = new SelectList(db.tb_lecturer, "IC", "Name", tb_evaluator.EvaluatorIC);
            return View(tb_evaluator);
        }

        // GET: evaluator/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_evaluator tb_evaluator = db.tb_evaluator.Find(id);
            if (tb_evaluator == null)
            {
                return HttpNotFound();
            }
            ViewBag.EvaluatorIC = new SelectList(db.tb_lecturer, "IC", "Name", tb_evaluator.EvaluatorIC);
            return View(tb_evaluator);
        }

        // POST: evaluator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvaluatorIC,OwnedStudentIC")] tb_evaluator tb_evaluator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_evaluator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EvaluatorIC = new SelectList(db.tb_lecturer, "IC", "Name", tb_evaluator.EvaluatorIC);
            return View(tb_evaluator);
        }

        // GET: evaluator/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_evaluator tb_evaluator = db.tb_evaluator.Find(id);
            if (tb_evaluator == null)
            {
                return HttpNotFound();
            }
            return View(tb_evaluator);
        }

        // POST: evaluator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_evaluator tb_evaluator = db.tb_evaluator.Find(id);
            db.tb_evaluator.Remove(tb_evaluator);
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
