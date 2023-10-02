using BillingRegister.Models;
using BillingRegister.Models.ViewModels;
using Complain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingRegister.Controllers
{
    public class BillTransactionController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: BillTransaction
        public ActionResult CreateBillTransaction()
        {
            return View();
        }
        public ActionResult TransactionInfo(int Id)
        {
            var response = new CommonVM();
            try
            {
                string sp = "exec GetAllTransactionById " + Id + "";
                var TransList = context.Database.SqlQuery<BillingRegisterVM>(sp).ToList();
                var AllData = new
                {
                    TransList = TransList,
                };
                var result = Json(JsonConvert.SerializeObject(AllData));
                return result;
            }
            catch (Exception e)
            {
                response.StatusCode = "-1";
                response.StatusMessage = e.ToString();
                var result1 = Json(JsonConvert.SerializeObject(response));
                return result1;
            }

        }
    }
}