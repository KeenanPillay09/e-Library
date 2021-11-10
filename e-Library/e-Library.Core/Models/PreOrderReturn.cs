using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.Models
{
    public class PreOrderReturn : BaseEntity
    {
        public string OrderID { get; set; }


        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        public string Email { get; set; }

        public string Reason { get; set; }

        [DisplayName("Refund Type")]
        public refundTypes RefundType { get; set; }

        public string Status { get; set; }


        public enum refundTypes
        {
            Cash,
            EFT
        }
    }
}
