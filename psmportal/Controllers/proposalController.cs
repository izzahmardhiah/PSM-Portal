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
    public class proposalController : Controller
    {
        private db_psmportalEntities db = new db_psmportalEntities();

        // GET: proposal
        public ActionResult Index()
        {
            var tb_proposal = db.tb_proposal.Include(t => t.tb_evaluator).Include(t => t.tb_evaluator1);
            return View(tb_proposal.ToList());
        }

        // GET: proposal/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_proposal tb_proposal = db.tb_proposal.Find(id);
            if (tb_proposal == null)
            {
                return HttpNotFound();
            }
            return View(tb_proposal);
        }

        // GET: proposal/Create
        public ActionResult Create()
        {
            ViewBag.Evaluator1 = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC");
            ViewBag.Evaluator2 = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC");
            return View();
        }

        // POST: proposal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProposalID,Title,ProposalDoc,DateUploaded,Notes,Evaluator1,Evaluator2,StudentIC")] tb_proposal tb_proposal)
        {
            if (ModelState.IsValid)
            {
                db.tb_proposal.Add(tb_proposal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Evaluator1 = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC", tb_proposal.Evaluator1);
            ViewBag.Evaluator2 = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC", tb_proposal.Evaluator2);
            return View(tb_proposal);
        }

        // GET: proposal/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_proposal tb_proposal = db.tb_proposal.Find(id);
            if (tb_proposal == null)
            {
                return HttpNotFound();
            }
            ViewBag.Evaluator1 = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC", tb_proposal.Evaluator1);
            ViewBag.Evaluator2 = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC", tb_proposal.Evaluator2);
            return View(tb_proposal);
        }

        // POST: proposal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProposalID,Title,ProposalDoc,DateUploaded,Notes,Evaluator1,Evaluator2,StudentIC")] tb_proposal tb_proposal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_proposal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Evaluator1 = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC", tb_proposal.Evaluator1);
            ViewBag.Evaluator2 = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC", tb_proposal.Evaluator2);
            return View(tb_proposal);
        }

        // GET: proposal/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_proposal tb_proposal = db.tb_proposal.Find(id);
            if (tb_proposal == null)
            {
                return HttpNotFound();
            }
            return View(tb_proposal);
        }

        // POST: proposal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_proposal tb_proposal = db.tb_proposal.Find(id);
            db.tb_proposal.Remove(tb_proposal);
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
