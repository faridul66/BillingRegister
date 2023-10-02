using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingRegister.Models.ViewModels
{
    public class BillingRegisterVM
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string AssignPerson { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string InvoiceNo { get; set; }
        public string EntryNo { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal BillAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Status { get; set; }
    }
    public class BillingInfo
    {
        public int Id { get; set; }
    }
}