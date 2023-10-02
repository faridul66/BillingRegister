using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complain.Models.ViewModels
{
    public class INVENTDIMVM
    {
        public string INVENTDIMID { get; set; }
        public string ITEMID { get; set; }
        public string ITEMNAME { get; set; }
        public string Spec1 { get; set; }
        public string Spec2 { get; set; }
        public string Spec3 { get; set; }
        public decimal OrderQty { get; set; }
        public string Unit { get; set; }
        public DateTime DELIVERYDATE { get; set; }
        public string DeliverySiteName { get; set; }
    }
}