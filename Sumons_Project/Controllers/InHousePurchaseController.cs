using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingRegister.Controllers
{
    public class InHousePurchaseController : Controller
    {
        // GET: InHousePurchase
        public ActionResult Create()
        {
            return View();
        }
    }
}