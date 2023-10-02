using BillingRegister.Models;
using BillingRegister.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingRegister.Controllers
{
    public class BillingInfoReportController : Controller
    {
        // GET: BillingInfoReport
        ApplicationDbContext context = new ApplicationDbContext();
        ApplicationDbContextBilling db = new ApplicationDbContextBilling();
        public ActionResult CreateBillingInfoReport()
        {
            return View();
        }
        public ActionResult GetAllDropdownData()
        {
            var response = new CommonVM();
            try
            {
                string sp3 = "select StatusCode,StatusName from Statustbl";
                var StatusData = context.Database.SqlQuery<StatusVM>(sp3).ToList();
                var AllData = new
                {
                    StatusData = StatusData,
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
        public ActionResult rptBillingInfo()
        {
            var EmpId = User.Identity.GetUserId();
            var usr = context.Users.Find(EmpId);
            string FromDate = Request.QueryString["FromDate"];
            string ToDate = Request.QueryString["ToDate"];
            string Status = Request.QueryString["Status"];
            string sp = "exec GetAllBillingInfo_for_Rpt'" + FromDate + "','" + ToDate + "','"+ Status + "','"+ usr.UserName + "'";
            var response1 = context.Database.SqlQuery<BillingRegisterVM>(sp).ToList();
            //ViewBag.CompanyName = response1[0].CompanyName.Split('[')[0];
            //ViewBag.BankGuaranteeNumber = response1[0].CustomerName.Split('[')[0];
            return new Rotativa.ViewAsPdf("rptBillingInfo", response1)
            {
                CustomSwitches = "--footer-left \"Reporting Date: " + DateTime.Now.ToString("dd-MM-yyyy") + "\" " + "--footer-right \"Page: [page] of [toPage]\"        --footer-font-size \"9\" --footer-spacing 5  --footer-font-name \"calibri light\""
            };
        }
    }
}