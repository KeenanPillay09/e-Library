using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.Models
{
    public class PreOrder : BaseEntity
    {
        //public PreOrder()
        //{
        //    this.OrderItems = new List<PreOrderItem>();
        //}

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
        [DisplayName("BookId")]
        public string BookId { get; set; }
        [DisplayName("Book Name")]
        public string BookName { get; set; }
        public deliveryType Delivery { get; set; } //Added Enum Property
        [DisplayName("Delivery Method")]
        public string DeliveryMethod { get; set; }
        [DisplayName("Basket Total")]
        public decimal BasketTotal { get; set; }
        public string Driver { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
        //public virtual ICollection<PreOrderItem> OrderItems { get; set; }
        [DisplayName("Suburb")]
        public string Suburb { get; set; }
        public areaList Area { get; set; }

        public enum deliveryType
        {
            Courier,
            Collect
        }

        public enum areaList
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

             

        public string DetermineSuburb()
        {
            string sub = "";

            //
            if (Area == (areaList.Broadway)) { sub = "North"; }
            if (Area == (areaList.La_Lucia)) { sub = "North"; }
            if (Area == (areaList.Tongaat)) { sub = "North"; }
            if (Area == (areaList.Umdloti)) { sub = "North"; }
            if (Area == (areaList.Umhlanga)) { sub = "North"; }
            if (Area == (areaList.Verulam)) { sub = "North"; }

            //Central
            if (Area == (areaList.Asherville)) { sub = "Central"; }
            if (Area == (areaList.Berea)) { sub = "Central"; }
            if (Area == (areaList.Bonela)) { sub = "Central"; }
            if (Area == (areaList.Cato_Manor)) { sub = "Central"; }
            if (Area == (areaList.Durban_North)) { sub = "Central"; }
            if (Area == (areaList.Essenwood)) { sub = "Central"; }
            if (Area == (areaList.Greyville)) { sub = "Central"; }
            if (Area == (areaList.Glenwood)) { sub = "Central"; }
            if (Area == (areaList.KwaMashu)) { sub = "Central"; }
            if (Area == (areaList.Mayville)) { sub = "Central"; }
            if (Area == (areaList.Merebank)) { sub = "Central"; }
            if (Area == (areaList.Mobeni)) { sub = "Central"; }
            if (Area == (areaList.Phoenix)) { sub = "Central"; }
            if (Area == (areaList.Prospecton)) { sub = "Central"; }
            if (Area == (areaList.Springfield)) { sub = "Central"; }
            if (Area == (areaList.Overport)) { sub = "Central"; }
            if (Area == (areaList.Stamford_Hill)) { sub = "Central"; }
            if (Area == (areaList.Wentworth)) { sub = "Central"; }
            if (Area == (areaList.Umlazi)) { sub = "Central"; }
            if (Area == (areaList.Umbilo)) { sub = "Central"; }
            if (Area == (areaList.Windermere)) { sub = "Central"; }
            if (Area == (areaList.Woodhaven)) { sub = "Central"; }
            if (Area == (areaList.Woodlands)) { sub = "Central"; }
            if (Area == (areaList.Yellowwood_Park)) { sub = "Central"; }

            //South

            if (Area == (areaList.Amanzimtoti)) { sub = "South"; }
            if (Area == (areaList.Athlone)) { sub = "South"; }
            if (Area == (areaList.Bluff)) { sub = "South"; }
            if (Area == (areaList.Illovo)) { sub = "South"; }
            if (Area == (areaList.Isipingo)) { sub = "South"; }
            if (Area == (areaList.Umkomaas)) { sub = "South"; }

            //Outer West
            if (Area == (areaList.Assagay)) { sub = "Outer West"; }
            if (Area == (areaList.Bothas_Hill)) { sub = "Outer West"; }
            if (Area == (areaList.Cato_Ridge)) { sub = "Outer West"; }
            if (Area == (areaList.Forest_Hills)) { sub = "Outer West"; }
            if (Area == (areaList.Gillets)) { sub = "Outer West"; }
            if (Area == (areaList.Hillcrest)) { sub = "Outer West"; }
            if (Area == (areaList.Kloof)) { sub = "Outer West"; }
            if (Area == (areaList.Shongweni)) { sub = "Outer West"; }
            if (Area == (areaList.Waterfall)) { sub = "Outer West"; }
            if (Area == (areaList.Wyebank)) { sub = "Outer West"; }

            //Inner West
            if (Area == (areaList.Clermont)) { sub = "Inner West"; }
            if (Area == (areaList.Cowies_Hill)) { sub = "Inner West"; }
            if (Area == (areaList.Malvern)) { sub = "Inner West"; }
            if (Area == (areaList.Mariannhill)) { sub = "Inner West"; }
            if (Area == (areaList.Pinetown)) { sub = "Inner West"; }
            if (Area == (areaList.Chatsworth)) { sub = "Inner West"; }
            if (Area == (areaList.Bellaire)) { sub = "Inner West"; }
            if (Area == (areaList.Avoca)) { sub = "Inner West"; }
            if (Area == (areaList.Queensburgh)) { sub = "Inner West"; }
            if (Area == (areaList.Palmiet)) { sub = "Inner West"; }
            if (Area == (areaList.Reservoir_Hills)) { sub = "Inner West"; }
            if (Area == (areaList.Sea_Cow_Lake)) { sub = "Inner West"; }
            if (Area == (areaList.Shallcross)) { sub = "Inner West"; }
            if (Area == (areaList.Westville)) { sub = "Inner West"; }


            return sub;
        }
    }
}