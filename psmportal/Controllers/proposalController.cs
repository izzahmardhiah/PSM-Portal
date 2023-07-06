using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using psmportal.Models;
using psmportal.Models.ViewModels;
using static System.Net.WebRequestMethods;

namespace psmportal.Controllers
{
    public class proposalController : Controller
    {
        private db_psmportalEntities1 db = new db_psmportalEntities1();

        // GET: proposal
        public ActionResult Index()
        {
            var studentIC = Session["IC"]?.ToString();

            var tb_proposal = db.tb_proposal.Include(t => t.tb_student); // Include the tb_student navigation property

            // Get the student's supervisor IC
            var supervisorIC = db.tb_sv.Where(sv => sv.OwnedStudentIC == studentIC)
                                       .Select(sv => sv.SupervisorIC)
                                       .FirstOrDefault();

            // Get the list of evaluators from tb_lecturer excluding the supervisor
            var evaluators = db.tb_lecturer.Where(l => l.IC != supervisorIC)
                                           .ToList();

            // Exclude evaluators who match the supervisor IC from tb_sv
            var excludedEvaluators = db.tb_lecturer.Where(l => l.IC == supervisorIC)
                                                   .ToList();
            evaluators = evaluators.Except(excludedEvaluators).ToList();

            ViewBag.Evaluator1 = new SelectList(evaluators, "IC", "Name");
            ViewBag.Evaluator2 = new SelectList(evaluators, "IC", "Name");

            return View(tb_proposal.ToList());
        }

        public ActionResult IndexEvaluator()
        {
            var studentIC = Session["IC"]?.ToString();

            var tb_proposal = db.tb_proposal.Include(t => t.tb_student); // Include the tb_student navigation property

            // Get the student's supervisor IC
            var supervisorIC = db.tb_sv.Where(sv => sv.OwnedStudentIC == studentIC)
                                       .Select(sv => sv.SupervisorIC)
                                       .FirstOrDefault();

            // Get the list of evaluators from tb_lecturer excluding the supervisor
            var evaluators = db.tb_lecturer.Where(l => l.IC != supervisorIC)
                                           .ToList();

            // Exclude evaluators who match the supervisor IC from tb_sv
            var excludedEvaluators = db.tb_lecturer.Where(l => l.IC == supervisorIC)
                                                   .ToList();
            evaluators = evaluators.Except(excludedEvaluators).ToList();

            ViewBag.Evaluator1 = new SelectList(evaluators, "IC", "Name");
            ViewBag.Evaluator2 = new SelectList(evaluators, "IC", "Name");

            return View(tb_proposal.ToList());
        }

        [HttpPost]
        public ActionResult UpdateEvaluators(string proposalID, string evaluator1, string evaluator2)
        {
            var proposal = db.tb_proposal.Find(proposalID);
            if (proposal != null)
            {
                try
                {
                    // Create new tb_evaluator records
                    if (!string.IsNullOrEmpty(evaluator1))
                    {
                        var evaluator1Record = new tb_evaluator
                        {
                            EvaluatorIC = evaluator1,
                            
                        };
                        db.tb_evaluator.Add(evaluator1Record);
                    }

                    if (!string.IsNullOrEmpty(evaluator2))
                    {
                        var evaluator2Record = new tb_evaluator
                        {
                            EvaluatorIC = evaluator2,
                                                    };
                        db.tb_evaluator.Add(evaluator2Record);
                    }

                    db.SaveChanges(); // Save changes to tb_evaluator

                    // Update tb_proposal
                    proposal.Evaluator1 = evaluator1;
                    proposal.Evaluator2 = evaluator2;
                    db.SaveChanges(); // Save changes to tb_proposal
                }
                catch (DbEntityValidationException ex)
                {
                    // Handle validation errors
                    foreach (var entityValidationError in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationError.ValidationErrors)
                        {
                            // Log or handle validation error
                            var errorMessage = $"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}";
                            // Handle the error as needed
                        }
                    }

                    // Return an appropriate response or error view
                    // For example:
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                    return View(); // or return an error view
                }
            }

            return RedirectToAction("Index");
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
            var model = new tb_proposal();
            return View(model);
        }


        // POST: proposal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tb_proposal tb_proposal, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(file.FileName);
                string _path1 = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                file.SaveAs(_path1);
                tb_proposal.ProposalDoc = _FileName;
            }

            if (ModelState.IsValid)
            {
                // Set the DateUploaded to the current date
                tb_proposal.DateUploaded = DateTime.Now;

                // Get the StudentIC from the session variable
                var studentIC = Session["IC"]?.ToString();
                tb_proposal.StudentIC = studentIC;

                // No longer generating ProposalID, it will be set based on the hidden field value

                db.tb_proposal.Add(tb_proposal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Handle other scenarios or return the view with validation errors
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
                var existingProposal = db.tb_proposal.Find(tb_proposal.ProposalID);
                if (existingProposal != null)
                {
                    // Update the Notes field
                    existingProposal.Notes = tb_proposal.Notes;

                    db.Entry(existingProposal).State = EntityState.Modified;
                    db.SaveChanges();
                }
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
