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
    public class programController : Controller
    {
        private db_psmportalEntities db = new db_psmportalEntities();

        // GET: program
        public ActionResult Index()
        {
            return View(db.tb_program.ToList());
        }

        // GET: program/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_program tb_program = db.tb_program.Find(id);
            if (tb_program == null)
            {
                return HttpNotFound();
            }
            return View(tb_program);
        }

        // GET: program/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: program/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramID,ProgramName")] tb_program tb_program)
        {
            if (ModelState.IsValid)
            {
                db.tb_program.Add(tb_program);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_program);
        }

        // GET: program/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_program tb_program = db.tb_program.Find(id);
            if (tb_program == null)
            {
                return HttpNotFound();
            }
            return View(tb_program);
        }

        // POST: program/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProgramID,ProgramName")] tb_program tb_program)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_program).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_program);
        }

        // GET: program/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_program tb_program = db.tb_program.Find(id);
            if (tb_program == null)
            {
                return HttpNotFound();
            }
            return View(tb_program);
        }

        // POST: program/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_program tb_program = db.tb_program.Find(id);
            db.tb_program.Remove(tb_program);
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
