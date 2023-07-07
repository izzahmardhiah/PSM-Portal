using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psmportal.Models;
using System.Web.Helpers;

namespace psmportal.Controllers
{
   
    public class LoginController : Controller
    {
        db_psmportalEntities1 db = new db_psmportalEntities1();
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
                using (db_psmportalEntities1 db = new db_psmportalEntities1())
                {
                    var user = db.tb_user.FirstOrDefault(a => a.IC.Equals(objchk.IC));
                    if (user != null && Crypto.VerifyHashedPassword(user.Password, objchk.Password))
                    {
                        Session["IC"] = user.IC.ToString();
                        Session["Role"] = user.Role.ToString(); // Store the user's role in the session

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