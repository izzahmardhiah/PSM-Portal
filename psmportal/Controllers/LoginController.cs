using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psmportal.Models;

namespace psmportal.Controllers
{
    public class LoginController : Controller
    {
       db_psmportalEntities db = new db_psmportalEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index(tb_user objchk)
        {
            if (ModelState.IsValid)
            {
                using (db_psmportalEntities db = new db_psmportalEntities())
                {
                    var obj = db.tb_user.Where(a => a.IC.Equals(objchk.IC) && a.Password.Equals(objchk.Password)).FirstOrDefault();
                    if (obj != null) 
                    {
                        Session["IC"] = obj.IC.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "IC or password entered is incorrect");
                    }
                }
            }

            return View(objchk);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}