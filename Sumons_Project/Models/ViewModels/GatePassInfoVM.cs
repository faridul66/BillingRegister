using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complain.Models.ViewModels
{
    public class GatePassInfoVM
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public string SalesOrderNo { get; set; }
        public string ChallanNo { get; set; }
        public string DeliverySite { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string TransCompanyName { get; set; }
        public string TransName { get; set; }
        public string TransNumber { get; set; }
        
        public string TransType { get; set; }
        public string DriverName { get; set; }
        public string DriverMobile { get; set; }
        public string BankGuaranteeNumber { get; set; }
        public DateTime BankGuaranteeDate { get; set; }
        public string BankName { get; set; }
        public string TenderNumber { get; set; }
        public string PackageNumber { get; set; }
        public int LotNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Remarks { get; set; }
        public string ItemName { get; set; }
        public string GatePassNo { get; set; }
        public string description { get; set; }
        public string Unit { get; set; }
        public decimal qty { get; set; }
        public string ItemXml { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<ItemInfo> IteminfoList { get; set; }
    }
    public class ItemInfo
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Spec1 { get; set; }
        public string Spec2 { get; set; }
        public string Spec3 { get; set; }
        public decimal OrderQty { get; set; }
        public decimal DeliveryQty { get; set; }
        public string Unit { get; set; }
        public string Remarks { get; set; }
    }


}