using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingRegister.Models.ViewModels
{
    public class StatusVM
    {
        public int Id { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
    }
}