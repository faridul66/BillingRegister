using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingRegister.Models.ViewModels
{
    public class CompanyVM
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
    }
}