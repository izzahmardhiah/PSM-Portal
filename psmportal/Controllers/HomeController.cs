using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psmportal.Models;
using psmportal.Models.ViewModels;

namespace psmportal.Controllers
{
    public class HomeController : Controller
    {
        private db_psmportalEntities db = new db_psmportalEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Create()
        {
            var viewModel = new RequestCreateViewModel
            {
                Request = new tb_request(),
                Lecturers = db.tb_lecturer.ToList()
            };

            return View(viewModel);
        }

    }
}