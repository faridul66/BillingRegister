using BillingRegister.Models;
using BillingRegister.Models.ViewModels;
using Complain.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cloudscribe.Messaging.Sms;
using System.Web.Services.Description;
using Microsoft.Exchange.WebServices.Auth.Validation;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace BillingRegister.Controllers
{
    public class BillingEntryController : Controller
    {
        // GET: BillingEntry
        ApplicationDbContext context = new ApplicationDbContext();
        ApplicationDbContextBilling db = new ApplicationDbContextBilling();
        public ActionResult Create()
        {
            ViewBag.EntryDate = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }
        string cons = ConfigurationManager.ConnectionStrings["DynamicsERPForBilling"].ConnectionString;
        public ActionResult Email()
        {
            return View();
        }
        public ActionResult Send(string recipient, string subject, string message)
        {
            var response = new CommonVM();
            // Create a new SmtpClient object.
            SmtpClient client = new SmtpClient();
            client.Host = "bangjin.com";
            client.Port = 587;
            client.EnableSsl = true;
            

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("faridul.islam@bangjin.com", "Billing Register");
            mail.To.Add(new MailAddress(recipient, " "));

            // Set the subject and body of the email.
            mail.Subject = subject;
            mail.Body = message;

            

            NetworkCredential nc = new NetworkCredential("faridul.islam@bangjin.com", "osgJRn1Cl6");
            client.UseDefaultCredentials = true;
            client.Credentials = nc;
            client.Send(mail);

            response.StatusCode = "1";
            response.StatusMessage = "Successfully Send";

            var result = Json(JsonConvert.SerializeObject(response));
            return result;
        }
        public ActionResult GetAllDropdownData()
        {
            List<CustomerVM> cust = new List<CustomerVM>();
            List<Company> CompanyInfo = new List<Company>();
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            var response = new CommonVM();
            try
            {
                var EntryNo = "";
                int Increment = 1;
                var date = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime filterdate = Convert.ToDateTime(date);
                //string sp = "select ACCOUNTNUM CustCode,NAME+'['+ACCOUNTNUM+']' CustName,DATAAREAID from CUSTTABLE";
                //var CustomerInfo = db.Database.SqlQuery<CustomerVM>(sp).ToList();
                //string sp1 = "select ID CompanyCode,NAME +'['+ID+']' CompanyName from DATAAREA";
                //var CompanyInfo = db.Database.SqlQuery<Company>(sp1).ToList();
                using (SqlConnection con = new SqlConnection(cons))
                {
                    string query = "select ACCOUNTNUM CustCode,NAME+'['+ACCOUNTNUM+']' CustName,DATAAREAID from CUSTTABLE";
                    string sp1 = "select ID CompanyCode,NAME +'['+ID+']' CompanyName from DATAAREA";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(ds);
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                cust.Add(new CustomerVM
                                {
                                    CustName = row["CustName"].ToString(),
                                    CustCode = row["CustCode"].ToString(),
                                    DATAAREAID = row["DATAAREAID"].ToString()
                                });
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand(sp1))
                    {
                        cmd.Connection = con;
                        using (SqlDataAdapter sa = new SqlDataAdapter(cmd))
                        {
                            sa.Fill(ds1);
                            foreach (DataRow row in ds1.Tables[0].Rows)
                            {
                                CompanyInfo.Add(new Company
                                {
                                    CompanyName = row["CompanyName"].ToString(),
                                    CompanyCode = row["CompanyCode"].ToString()
                                });
                            }
                        }
                    }
                }
                string sp2 = "select EntryNo from BillingRegisterTbl where EntryDate='"+ filterdate + "'";
                var BillInfo = context.Database.SqlQuery<BillingRegisterVM>(sp2).ToList(); 
                if (BillInfo.Count==0)
                {
                    EntryNo=DateTime.Now.ToString("ddMMyyyy")+"_"+ Increment;
                }
                else
                {
                    EntryNo= DateTime.Now.ToString("ddMMyyyy") + "_" + (BillInfo.Count+1);
                }
                var AllData = new
                {
                    CompanyInfo= CompanyInfo,
                    CustomerInfo = cust,
                    EntryNo= EntryNo
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
        public ActionResult PostData(BillingRegisterVM saveData)
        {
            CommonVM response = new CommonVM();
            try
            {
                if (saveData != null)
                {
                    var EmpId = User.Identity.GetUserId();
                    var usr = context.Users.Find(EmpId);
                    response = SaveItemInfo(saveData, usr.UserName);
                    response.StatusCode = "1";
                    response.StatusMessage = "Saved Successfully";
                    var result = Json(JsonConvert.SerializeObject(response));
                    return result;
                }
            }
            catch (Exception e)
            {
                response.StatusCode = "-1";
                response.StatusMessage = e.ToString();

            }
            var result1 = Json(JsonConvert.SerializeObject(response));
            return result1;
        }
        public CommonVM SaveItemInfo(BillingRegisterVM Data, string EmpId)
        {
            CommonVM VM_SP_Results = new CommonVM();
            string sp = "exec Insert_Update_BillingRegister '" + Data.Id + "','" + Data.CustomerName + "','" + Data.CompanyName + "'," +
                "'" + Data.PurchaseOrderNo + "','" + Data.EntryDate + "','" + Data.BillDate + "','" + Data.EntryNo + "','" + Data.BillAmount + "','"+EmpId+"'";
            var response1 = context.Database.SqlQuery<CommonVM>(sp).FirstOrDefault();
            return response1;
        }
    }
}