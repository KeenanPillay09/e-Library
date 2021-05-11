using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.Models
{
    public class Book
    {
        public string Id { get; set; }

        [DisplayName("Book Name")]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Author { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public string Genre { get; set; }

        [Range(0,5000)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }

        public Book()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
