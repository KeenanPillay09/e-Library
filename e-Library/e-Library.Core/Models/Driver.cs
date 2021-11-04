using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.Models
{
    public class Driver : BaseEntity
    {
        [DisplayName("Driver Name")]
        public string DriverName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public SuburbType Suburb { get; set; }

        public enum SuburbType
        {
            North,
            Central,
            South,
            [Display(Name = "Outer West")]
            Outer_West,
            [Display(Name = "Inner West")]
            Inner_West
        }
    }
}
