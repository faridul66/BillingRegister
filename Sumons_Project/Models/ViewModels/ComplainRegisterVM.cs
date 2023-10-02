using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complain.Models.ViewModels
{
    public class ComplainRegisterVM
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string SalesOrderNo { get; set; }
        public string ChallanNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ComplainDate { get; set; }
        public string DeliveryFrom { get; set; }
        public string Manufacturedby { get; set; }
        public string CustomerComplain { get; set; }
        public string Status { get; set; }
        public string AssignPersons { get; set; }
        public string TransNumber { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string Modifier { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}