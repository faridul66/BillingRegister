using BillingRegister.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using System.Threading;

namespace BillingRegister.Controllers
{[Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        private ApplicationDbContext db = new ApplicationDbContext();


        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Picval()
        {
            string userIdq = User.Identity.GetUserId();
            var user = UserManager.FindById(userIdq);
            string Picval = user.PictureFile;
            if (Picval==null || Picval == "")
            {
                string Pic = "Blank-Profile-Pic.PNG";
                return Content(Pic);
            }
            else
            {
                string Pic = Picval;
                return Content(Pic);

            }
            
        }
        public ActionResult FullName()
        {
            string userIdq = User.Identity.GetUserId();
            var user = UserManager.FindById(userIdq);
            string fullname = user.FirstName+" "+user.LastName;
            return Content(fullname);

        }
        public ActionResult FirstName()
        {
            string userIdq = User.Identity.GetUserId();
            var user = UserManager.FindById(userIdq);
            string firstname = user.FirstName;
            return Content(firstname);
        }


        [Authorize]
        public ActionResult Ping()
        {
            return Json(new { message = "Magic! Magic! Magic! :P" }, JsonRequestBehavior.AllowGet);
        }

    }
}
