using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.Models
{
    public class Order: BaseEntity
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }

        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [DisplayName("Order Status")]
        public string OrderStatus { get; set; }
        public DeliveryType Delivery { get; set; } //Added Enum Property
        [DisplayName("Delivery Method")]
        public string DeliveryMethod { get; set; }
        [DisplayName("Basket Total")]
        public decimal BasketTotal { get; set; }
        [DisplayName("Final Total")]
        public decimal FinalTotal { get; set; }
        public string Driver { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public enum DeliveryType
        {
            Courier,
            Collect
        }

        public decimal CalcOrderFinalTotal()
        {
            decimal finaltotal = BasketTotal;

            if (BasketTotal >= 350)
            {
                //Free
            }
            else  // No Free Delivery
            {
                if (Delivery == DeliveryType.Courier)
                {
                    if (DeliveryMethod == "Standard Delivery")
                    {
                        finaltotal += 60;
                    }
                    else
                    if (DeliveryMethod == "Express Delivery")
                    {
                        finaltotal += 95;
                    }
                }
                else

                if (Delivery == DeliveryType.Collect)
                {
                    if (DeliveryMethod == "Normal Collection")
                    {
                        finaltotal += 25;
                    }
                    else
                    if (DeliveryMethod == "Delayed Collection")
                    {
                        finaltotal += 50;
                    }
                }
            }

            return finaltotal;
        }
    }
}

//Add DeliveryType
//Add Delivery Method
//Add Basket Total
//Add Final Total