using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.Models
{
    public class BookAuthor
    {
        public string Id { get; set; }
        public string Author { get; set; }

        public BookAuthor()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
