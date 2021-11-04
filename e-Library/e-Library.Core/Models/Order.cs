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
        [DisplayName("Suburb")]
        public string Suburb { get; set; }
        public AreaList Area { get; set; }

        public enum DeliveryType
        {
            Courier,
            Collect
        }

        public enum AreaList
        {
            Amanzimtoti,
            Asherville,
            Assagay,
            Athlone,
            Avoca,
            Bellaire,
            Berea,
            Bluff,
            Bonela,
            [Display(Name = "Botha's Hill")]
            Bothas_Hill,
            Broadway,
            [Display(Name = "Cato Manor")]
            Cato_Manor,
            [Display(Name = "Cato Ridge")]
            Cato_Ridge,
            Clermont,
            [Display(Name = "Cowies Hill")]
            Cowies_Hill,
            Chatsworth,
            [Display(Name = "Durban North")]
            Durban_North,
            Essenwood,
            [Display(Name = "Forest Hills")]
            Forest_Hills,
            Gillets,
            Glenwood,
            Greyville,
            Hillcrest,
            Illovo,
            Isipingo,
            Kennedy,
            Kenville,
            Kloof,
            KwaMashu,
            [Display(Name = "La Lucia")]
            La_Lucia,
            [Display(Name = "Lotus Park")]
            Lotus_Park,
            Malvern,
            Magabeni,
            Mariannhill,
            Mayville,
            Montclair,
            Morningside,
            Merebank,
            Mobeni,
            [Display(Name = "Mount Vernon")]
            Mount_Vernon,
            Musgrave,
            [Display(Name = "North Beach")]
            North_Beach,
            Ottowa,
            Overport,
            Palmiet,
            Pinetown,
            Phoenix,
            Prospecton,
            Queensburgh,
            [Display(Name = "Rainbow Gardens")]
            Rainbow_Gardens,
            [Display(Name = "Reservoir Hills")]
            Reservoir_Hills,
            Ridgeview,
            [Display(Name = "Sea Cow Lake")]
            Sea_Cow_Lake,
            Shallcross,
            Sherwood,
            Shongweni,
            [Display(Name = "South Beach")]
            South_Beach,
            Springfield,
            [Display(Name = "Stamford Hill")]
            Stamford_Hill,
            Sydenham,
            Tongaat,
            Umbilo,
            Umdloti,
            [Display(Name = "Umgeni Park")]
            Umkomaas,
            Umlazi,
            Umhlanga,
            [Display(Name = "Upper Highway Area")]
            Verulam,
            Waterfall,
            Wentworth,
            Westville,
            Windermere,
            [Display(Name = "Windston Park")]
            Woodhaven,
            Woodlands,
            Wyebank,
            [Display(Name = "Yellowwood Park")]
            Yellowwood_Park
        }

        [DisplayName("Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime? DeliveryDate { get; set; }
        public string QRCode { get; set; }

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

        public DateTime CalcDeliveryDate()
        {
            DateTime deliveryDate = DateTime.Now;

            if (Delivery == DeliveryType.Courier)
            {
                if (DeliveryMethod == "Standard Delivery")
                {
                    deliveryDate = deliveryDate.AddDays(4);
                }
                else
                if (DeliveryMethod == "Express Delivery")
                {
                    deliveryDate = deliveryDate.AddDays(1);
                }
            }
            else

            if (Delivery == DeliveryType.Collect)
            {
                if (DeliveryMethod == "Normal Collection")
                {
                    deliveryDate = deliveryDate.AddDays(4);
                }
                else
                if (DeliveryMethod == "Delayed Collection")
                {
                    deliveryDate = deliveryDate.AddDays(7);
                }
            }

            return deliveryDate;
        }

        public string DetermineSuburb()
        {
            string sub = "";

            //
            if (Area == (AreaList.Broadway)) { sub = "North"; }
            if (Area == (AreaList.La_Lucia)) { sub = "North"; }
            if (Area == (AreaList.Tongaat)) { sub = "North"; }
            if (Area == (AreaList.Umdloti)) { sub = "North"; }
            if (Area == (AreaList.Umhlanga)) { sub = "North"; }
            if (Area == (AreaList.Verulam)) { sub = "North"; }

            //Central
            if (Area == (AreaList.Asherville)) { sub = "Central"; }
            if (Area == (AreaList.Berea)) { sub = "Central"; }
            if (Area == (AreaList.Bonela)) { sub = "Central"; }
            if (Area == (AreaList.Cato_Manor)) { sub = "Central"; }
            if (Area == (AreaList.Durban_North)) { sub = "Central"; }
            if (Area == (AreaList.Essenwood)) { sub = "Central"; }
            if (Area == (AreaList.Greyville)) { sub = "Central"; }
            if (Area == (AreaList.Glenwood)) { sub = "Central"; }
            if (Area == (AreaList.KwaMashu)) { sub = "Central"; }
            if (Area == (AreaList.Mayville)) { sub = "Central"; }
            if (Area == (AreaList.Merebank)) { sub = "Central"; }
            if (Area == (AreaList.Mobeni)) { sub = "Central"; }
            if (Area == (AreaList.Phoenix)) { sub = "Central"; }
            if (Area == (AreaList.Prospecton)) { sub = "Central"; }
            if (Area == (AreaList.Springfield)) { sub = "Central"; }
            if (Area == (AreaList.Overport)) { sub = "Central"; }
            if (Area == (AreaList.Stamford_Hill)) { sub = "Central"; }
            if (Area == (AreaList.Wentworth)) { sub = "Central"; }
            if (Area == (AreaList.Umlazi)) { sub = "Central"; }
            if (Area == (AreaList.Umbilo)) { sub = "Central"; }
            if (Area == (AreaList.Windermere)) { sub = "Central"; }
            if (Area == (AreaList.Woodhaven)) { sub = "Central"; }
            if (Area == (AreaList.Woodlands)) { sub = "Central"; }
            if (Area == (AreaList.Yellowwood_Park)) { sub = "Central"; }

            //South

            if (Area == (AreaList.Amanzimtoti)) { sub = "South"; }
            if (Area == (AreaList.Athlone)) { sub = "South"; }
            if (Area == (AreaList.Bluff)) { sub = "South"; }
            if (Area == (AreaList.Illovo)) { sub = "South"; }
            if (Area == (AreaList.Isipingo)) { sub = "South"; }
            if (Area == (AreaList.Umkomaas)) { sub = "South"; }

            //Outer West
            if (Area == (AreaList.Assagay)) { sub = "Outer West"; }
            if (Area == (AreaList.Bothas_Hill)) { sub = "Outer West"; }
            if (Area == (AreaList.Cato_Ridge)) { sub = "Outer West"; }
            if (Area == (AreaList.Forest_Hills)) { sub = "Outer West"; }
            if (Area == (AreaList.Gillets)) { sub = "Outer West"; }
            if (Area == (AreaList.Hillcrest)) { sub = "Outer West"; }
            if (Area == (AreaList.Kloof)) { sub = "Outer West"; }
            if (Area == (AreaList.Shongweni)) { sub = "Outer West"; }
            if (Area == (AreaList.Waterfall)) { sub = "Outer West"; }
            if (Area == (AreaList.Wyebank)) { sub = "Outer West"; }

            //Inner West
            if (Area == (AreaList.Clermont)) { sub = "Inner West"; }
            if (Area == (AreaList.Cowies_Hill)) { sub = "Inner West"; }
            if (Area == (AreaList.Malvern)) { sub = "Inner West"; }
            if (Area == (AreaList.Mariannhill)) { sub = "Inner West"; }
            if (Area == (AreaList.Pinetown)) { sub = "Inner West"; }
            if (Area == (AreaList.Chatsworth)) { sub = "Inner West"; }
            if (Area == (AreaList.Bellaire)) { sub = "Inner West"; }
            if (Area == (AreaList.Avoca)) { sub = "Inner West"; }
            if (Area == (AreaList.Queensburgh)) { sub = "Inner West"; }
            if (Area == (AreaList.Palmiet)) { sub = "Inner West"; }
            if (Area == (AreaList.Reservoir_Hills)) { sub = "Inner West"; }
            if (Area == (AreaList.Sea_Cow_Lake)) { sub = "Inner West"; }
            if (Area == (AreaList.Shallcross)) { sub = "Inner West"; }
            if (Area == (AreaList.Westville)) { sub = "Inner West"; }


            return sub;
        }
    }
}