using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    }
}
