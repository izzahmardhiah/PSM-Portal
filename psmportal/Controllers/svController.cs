using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using psmportal.Models;
using SelectPdf;
using System.IO;


namespace psmportal.Controllers
{
    public class svController : Controller
    {
        private db_psmportalEntities1 db = new db_psmportalEntities1();

        // GET: sv
        public ActionResult Index()
        {
            var tb_sv = db.tb_sv.Include(t => t.tb_lecturer);
            return View(tb_sv.ToList());
        }

        // GET: sv/Details/5
        public ActionResult Details()
        {
            var tb_student = db.tb_student.FirstOrDefault(); // Assuming there is only one tb_student record

            if (tb_student == null)
            {
                return HttpNotFound();
            }

            ViewBag.HasSupervisor = string.IsNullOrEmpty(tb_student.Supervisor);

            return View(tb_student);
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

        public ActionResult GenerateAgreement()
        {
            var tb_student = db.tb_student.FirstOrDefault(); // Assuming there is only one tb_student record
            var tb_sv = db.tb_sv.Include(t => t.tb_lecturer).FirstOrDefault(); // Assuming there is only one tb_sv record

            string studentName = tb_student != null ? tb_student.Name : "";
            string supervisorName = tb_sv != null && tb_sv.tb_lecturer != null ? tb_sv.tb_lecturer.Name : "";

            // Generate the PDF
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            HtmlToPdf converter = new HtmlToPdf();
            string htmlString = "<html><body>" +
             "<img src=\"https://comp.utm.my/psm/wp-content/blogs.dir/2566/files/2020/01/PSM-logo.png\">" +
             "<h1>Student and Supervisor Agreement</h1>" +
             "<p>Student: " + studentName + "</p>" +
             "<p>Matrics No.: " + (tb_student != null ? tb_student.MatricNo : "") + "</p>" +
             "<p>Supervisor: " + supervisorName + "</p>" +
             "<p>IC: " + tb_sv.SupervisorIC + "</p>" +
             "<p>This agreement is made between " + studentName + " (hereinafter referred to as \"the Student\") " +
             "and " + supervisorName + " (hereinafter referred to as \"the Supervisor\") for the purpose of supervising " +
             "the student's Final Year Project (FYP).</p>" +
             "<h2>Responsibilities of the Student:</h2>" +
             "<ol>" +
             "<li>The Student agrees to actively participate in the [project/thesis/dissertation] under the guidance of the Supervisor.</li>" +
             "<li>The Student will meet regularly with the Supervisor to discuss the progress, challenges, and any necessary modifications to the project.</li>" +
             "<li>The Student will adhere to the academic and ethical guidelines set by the institution and maintain the highest standards of research integrity.</li>" +
             "<li>The Student will submit progress reports, drafts, and final deliverables as per the agreed-upon timeline.</li>" +
             "</ol>" +
             "<h2>Responsibilities of the Supervisor:</h2>" +
             "<ol>" +
             "<li>The Supervisor agrees to provide guidance and support to the Student throughout the duration of the [project/thesis/dissertation].</li>" +
             "<li>The Supervisor will meet with the Student on a regular basis to provide feedback, suggestions, and direction for the project.</li>" +
             "<li>The Supervisor will assist the Student in identifying relevant resources, references, and research methodologies.</li>" +
             "<li>The Supervisor will review and provide constructive feedback on the drafts and final deliverables submitted by the Student.</li>" +
             "</ol>" +
             "<h2>Duration and Termination:</h2>" +
             "<ol>" +
             "<li>This agreement will be in effect from [Start Date] to [End Date].</li>" +
             "<li>Either party has the right to terminate this agreement by providing written notice to the other party.</li>" +
             "<li>In case of termination, the Student may be required to find an alternate Supervisor or adjust the scope of the project as per the institution's guidelines.</li>" +
             "</ol>" +
             "<h2>Confidentiality and Intellectual Property:</h2>" +
             "<ol>" +
             "<li>Both the Student and the Supervisor agree to maintain the confidentiality of any sensitive information shared during the course of the project.</li>" +
             "<li>Any intellectual property rights arising from the project will be discussed and agreed upon separately, as per the institution's policies and guidelines.</li>" +
             "</ol>" +
             "</body></html>";

            PdfDocument pdf = converter.ConvertHtmlString(htmlString);
            MemoryStream stream = new MemoryStream();
            pdf.Save(stream);
            pdf.Close();

            // Return the PDF file
            stream.Position = 0;
            return File(stream, "application/pdf", "Agreement.pdf");
        }

    }
}
