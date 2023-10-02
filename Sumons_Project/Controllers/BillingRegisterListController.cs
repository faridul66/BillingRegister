using BillingRegister.Models;
using BillingRegister.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BillingRegister.Controllers
{
    public class BillingRegisterListController : Controller
    {
        // GET: BillingRegisterList
        ApplicationDbContext context = new ApplicationDbContext();
        ApplicationDbContextBilling db = new ApplicationDbContextBilling();
        public ActionResult CreateBillingRegisterList()
        {
            
            return View();
        }
        public ActionResult GetAllDropdownData()
        {
            var response = new CommonVM();
            try
            {
                var role = "";
                if(User.IsInRole("Receive Section"))
                {
                    role = "Receive Section";
                }
                else if (User.IsInRole("Payment Section"))
                {
                    role = "Payment Section";
                }
                else if (User.IsInRole("Admin"))
                {
                    role = "Admin";
                }
                else
                {
                    role = "Entry Section";
                }
                var EmpId = User.Identity.GetUserId();
                var usr = context.Users.Find(EmpId);
                string sp3 = "exec GetAllBillingRegister '" + usr.UserName + "','"+ role + "'";
                var BillingRegisterData = context.Database.SqlQuery<BillingRegisterVM>(sp3).ToList();
                string sp = "select UserName as CustName from AspNetUsers";
                var UserList = context.Database.SqlQuery<CustomerVM>(sp).ToList();
                var AllData = new
                {
                    BillingRegisterData = BillingRegisterData,
                    UserList= UserList,
                    role= role
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
        public ActionResult PostData(List<BillingInfo> BillingInfo,string SendTo,string PaymentAmount)
        {
            CommonVM response = new CommonVM();
            try
            {
                if (BillingInfo != null)
                {
                    var recipient = "";
                    var role = "";
                    if (User.IsInRole("Receive Section"))
                    {
                        role = "Payment Section";
                        //recipient = "faridul982@gmail.com";
                    }
                    
                    else if (User.IsInRole("Payment Section"))
                    {
                        role = "Payment Complete";
                    }
                    else
                    {
                        role = "Receive Section";
                        recipient = "zobaerul.khandoker@bangjin.com";
                    }
                    string val = "";
                    for(int i=0;i< BillingInfo.Count;i++)
                    {
                        val = val+BillingInfo[i].Id + ",";
                    }
                    var updateId= val.Substring(0, val.Length - 1);
                    response = SaveItemInfo(updateId, SendTo,role, PaymentAmount, recipient);
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
        public CommonVM SaveItemInfo(string Data, string EmpId,string Role,string PaymentAmount,string recipient)
        {
            CommonVM VM_SP_Results = new CommonVM();
            var Emp = User.Identity.GetUserId();
            var usr = context.Users.Find(Emp);
            string sp1 = "";
            if (Role!= "Payment Complete")
            {
                SmtpClient client = new SmtpClient();
                client.Host = "bangjin.com";
                client.Port = 587;
                client.EnableSsl = true;
                if(Role== "Receive Section")
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("faridul.islam@bangjin.com", "Billing Register");
                    mail.To.Add(new MailAddress(recipient, " "));

                    // Set the subject and body of the email.
                    mail.Subject = "Prayer for Bill ";
                    mail.Body = "One or Multiple Bill send from Entry section and you click here or go to application link is http://192.168.1.11/billregister";
                    NetworkCredential nc = new NetworkCredential("faridul.islam@bangjin.com", "osgJRn1Cl6");
                    client.UseDefaultCredentials = true;
                    client.Credentials = nc;
                    client.Send(mail);
                }
                sp1 = "update BillingRegisterTbl set Status='" + Role + "',AssignPerson='" + EmpId + "' where Id in(" + Data + ")";
                var res = context.Database.SqlQuery<CommonVM>(sp1).FirstOrDefault();
                VM_SP_Results.StatusCode = "1";
                VM_SP_Results.StatusMessage = "Successfully Send";
            }
            else
            {
                //sp1 = "update BillingRegisterTbl set Status='" + Role + "' where Id in(" + Data + ")";
                sp1 = "Insert_Update_PaymentDetails "+Data+","+ Convert.ToDecimal(PaymentAmount) + ",'"+ usr.UserName + "'";
                VM_SP_Results = context.Database.SqlQuery<CommonVM>(sp1).FirstOrDefault();
            }

            
            return VM_SP_Results;
        }

        public ActionResult InvoicePost(string Invoice, int Id)
        {
            CommonVM response = new CommonVM();
            try
            {
                if (Id>0)
                {
                    var role = "";
                    if (User.IsInRole("Receive Section"))
                    {
                        role = "Payment Section";
                    }

                    else if (User.IsInRole("Payment Section"))
                    {
                        role = "Payment Complete";
                    }
                    else
                    {
                        role = "Receive Section";
                    }
                    response = InvoiceUpdate(Id, Invoice);
                    //response.StatusCode = "1";
                    //response.StatusMessage = "Saved Successfully";
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
        public CommonVM InvoiceUpdate(int Id,string Invoice)
        {
            CommonVM VM_SP_Results = new CommonVM();
            //string sp1 = "UpdateBillingRegister_Infoset Status='" + Role + "',AssignPerson='" + EmpId + "' where Id in(" + Data + ")";
            string sp = "UpdateBillingRegister_Info " + Id + ",'" + Invoice + "'";
            var response1 = context.Database.SqlQuery<CommonVM>(sp).FirstOrDefault();
            return response1;
        }

        public ActionResult ReturnData(int Id,string RecorRe)
        {
            CommonVM response = new CommonVM();
            try
            {
                if (Id>0)
                {
                    response = Return(Id,RecorRe);
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
        public CommonVM Return(int Id, string RecorRe)
        {
            CommonVM VM_SP_Results = new CommonVM();
            string sp1 = "";
            if (Id >0)
            {
                if (User.IsInRole("Receive Section"))
                {
                    if(RecorRe=="Return")
                    {
                        sp1 = "update BillingRegisterTbl set Status='Pending',AssignPerson='' where Id=" + Id + "";
                        VM_SP_Results.StatusMessage = "Successfully Return";
                    }
                    else
                    {
                        sp1 = "update BillingRegisterTbl set Status='Received' where Id=" + Id + "";
                        VM_SP_Results.StatusMessage = "Successfully Received";
                    }
                    VM_SP_Results.StatusCode = "1";
                    var res = context.Database.SqlQuery<CommonVM>(sp1).FirstOrDefault();
                }
                else
                {
                    VM_SP_Results.StatusCode = "2";
                    VM_SP_Results.StatusMessage = "You have no Permission to return this bill";
                }
                
            }
            
            return VM_SP_Results;
        }
        
    }
}