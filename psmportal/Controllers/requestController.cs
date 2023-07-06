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
    public class requestController : Controller
    {
        private db_psmportalEntities1 db = new db_psmportalEntities1();

        // GET: request
        public ActionResult Index()
        {
            var tb_request = db.tb_request.Include(t => t.tb_lecturer).Include(t => t.tb_student);
            return View(tb_request.ToList());
        }

        // GET: request/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_request tb_request = db.tb_request.Find(id);
            if (tb_request == null)
            {
                return HttpNotFound();
            }
            return View(tb_request);
        }

        // GET: request/Create
        // GET: request/Create
        public ActionResult Create()
        {
            var viewModel = new RequestCreateViewModel
            {
                Request = new tb_request
                {
                    // Set the default values for the properties
                    Status = 1,
                },
                Lecturers = db.tb_lecturer.ToList()
            };

            return View(viewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: request/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the StudentIC value from the hidden field
                viewModel.Request.StudentIC = viewModel.Request.StudentIC;

                // Set the default value for Status
                viewModel.Request.Status = 1;

                db.tb_request.Add(viewModel.Request);
                db.SaveChanges();
                return RedirectToAction("Index", "sv");
            }

            ViewBag.SupervisorIC = new SelectList(db.tb_lecturer, "IC", "Name", viewModel.Request.SupervisorIC);
            ViewBag.StudentIC = new SelectList(db.tb_student, "IC", "MatricNo", viewModel.Request.StudentIC);

            // Pass the view model to the view
            return View(viewModel);
        }


        // POST: request/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestID,StudentIC,SupervisorIC,Notes")] tb_request tb_request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupervisorIC = new SelectList(db.tb_lecturer, "IC", "Name", tb_request.SupervisorIC);
            ViewBag.StudentIC = new SelectList(db.tb_student, "IC", "MatricNo", tb_request.StudentIC);
            return View(tb_request);
        }

        // GET: request/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_request tb_request = db.tb_request.Find(id);
            if (tb_request == null)
            {
                return HttpNotFound();
            }
            return View(tb_request);
        }

        // POST: request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_request tb_request = db.tb_request.Find(id);
            db.tb_request.Remove(tb_request);
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

        [HttpPost]
        public ActionResult UpdateRequestStatus(int[] requestIds, int status)
        {
            if (requestIds != null && requestIds.Length > 0)
            {
                var requestsToUpdate = db.tb_request.Include("tb_student").Where(r => requestIds.Contains(r.RequestID));

                foreach (var request in requestsToUpdate)
                {
                    request.Status = status;

                    if (status == 2)
                    {
                        // Update Supervisor column in tb_student
                        request.tb_student.Supervisor = request.SupervisorIC;

                        // Insert a new row in tb_sv
                        tb_sv newSvRow = new tb_sv()
                        {
                            SupervisorIC = request.SupervisorIC,
                            OwnedStudentIC = request.StudentIC
                        };

                        db.tb_sv.Add(newSvRow);
                    }
                }

                db.SaveChanges();

                // Create a success message
                string message = (status == 2) ? "Requests approved successfully." : "Requests rejected successfully.";

                return Json(new { success = true, message = message });
            }

            return Json(new { success = false, message = "No requests selected." });
        }


    }
}
