using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.Models
{
    public class Return : BaseEntity
    {
        public string OrderID { get; set; }


        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        public string Email { get; set; }

        public string Reason { get; set; }

        [DisplayName("Refund Type")]
        public RefundTypes RefundType { get; set; }

        public string Status { get; set; }


        public enum RefundTypes
        {
            Cash,
            EFT
        }
    }
}
