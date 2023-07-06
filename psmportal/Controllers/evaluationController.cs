using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using psmportal.Models;
using psmportal.Models.ViewModels;

namespace psmportal.Controllers
{
    public class evaluationController : Controller
    {
        private db_psmportalEntities1 db = new db_psmportalEntities1();

        // GET: evaluation
        public ActionResult Index()
        {
            var tb_evaluation = db.tb_evaluation.Include(t => t.tb_proposal).Include(t => t.tb_status);
            return View(tb_evaluation.ToList());
        }

        // GET: evaluation/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_evaluation tb_evaluation = db.tb_evaluation.Find(id);
            if (tb_evaluation == null)
            {
                return HttpNotFound();
            }
            return View(tb_evaluation);
        }

        // GET: evaluation/Create
        public ActionResult Create(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                // Proposal ID is required, handle the error or redirect to an error page
                return RedirectToAction("Error");
            }

            ViewBag.EvaluatorIC = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC");
            ViewBag.EvaluationStatus = new SelectList(db.tb_status, "StatusID", "StatusName");

            // Fetch the tb_proposal record based on the ProposalID
            tb_proposal proposal = db.tb_proposal.Find(id);

            if (proposal == null)
            {
                // Proposal not found, handle the error or redirect to an error page
                return RedirectToAction("Error");
            }

            // Create a new tb_evaluation instance and set the ProposalID
            tb_evaluation evaluation = new tb_evaluation();
            evaluation.ProposalID = id;

            // Pass the evaluation and proposal to the view
            EvaluationProposalViewModel viewModel = new EvaluationProposalViewModel();
            viewModel.Evaluation = evaluation;
            viewModel.Proposal = proposal;

            return View(viewModel);
        }



        // POST: evaluation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvaluationID,EvaluatorIC,StudentIC,ProposalID,EvaluationComment,EvaluationMarks,EvaluationStatus")] tb_evaluation tb_evaluation)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the current user IC from the session
                string currentUserIC = Session["IC"].ToString();

                // Set the remaining properties of the tb_evaluation object
                tb_evaluation.EvaluatorIC = currentUserIC;

                // Save the tb_evaluation data to the database
                db.tb_evaluation.Add(tb_evaluation);
                db.SaveChanges();

                return RedirectToAction("IndexEvaluator", "Evaluation");
            }

            ViewBag.EvaluatorIC = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC", tb_evaluation.EvaluatorIC);
            ViewBag.ProposalID = new SelectList(db.tb_proposal, "ProposalID", "Title", tb_evaluation.ProposalID);
            ViewBag.EvaluationStatus = new SelectList(db.tb_status, "StatusID", "StatusName", tb_evaluation.EvaluationStatus);
            return View(tb_evaluation);
        }


        // GET: evaluation/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_evaluation tb_evaluation = db.tb_evaluation.Find(id);
            if (tb_evaluation == null)
            {
                return HttpNotFound();
            }
            ViewBag.EvaluatorIC = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC", tb_evaluation.EvaluatorIC);
            ViewBag.ProposalID = new SelectList(db.tb_proposal, "ProposalID", "Title", tb_evaluation.ProposalID);
            ViewBag.EvaluationStatus = new SelectList(db.tb_status, "StatusID", "StatusName", tb_evaluation.EvaluationStatus);
            return View(tb_evaluation);
        }

        // POST: evaluation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvaluationID,EvaluatorIC,StudentIC,ProposalID,EvaluationComment,EvaluationMarks,EvaluationStatus")] tb_evaluation tb_evaluation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_evaluation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EvaluatorIC = new SelectList(db.tb_evaluator, "EvaluatorIC", "EvaluatorIC", tb_evaluation.EvaluatorIC);
            ViewBag.ProposalID = new SelectList(db.tb_proposal, "ProposalID", "Title", tb_evaluation.ProposalID);
            ViewBag.EvaluationStatus = new SelectList(db.tb_status, "StatusID", "StatusName", tb_evaluation.EvaluationStatus);
            return View(tb_evaluation);
        }

        // GET: evaluation/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_evaluation tb_evaluation = db.tb_evaluation.Find(id);
            if (tb_evaluation == null)
            {
                return HttpNotFound();
            }
            return View(tb_evaluation);
        }

        // POST: evaluation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_evaluation tb_evaluation = db.tb_evaluation.Find(id);
            db.tb_evaluation.Remove(tb_evaluation);
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
