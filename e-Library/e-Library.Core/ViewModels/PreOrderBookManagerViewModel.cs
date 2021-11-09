using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_Library.Core.Models;

namespace e_Library.Core.ViewModels
{
    public class PreOrderBookManagerViewModel
    {
        public PreOrderBook Book { get; set; }
        public IEnumerable<BookGenre> BookGenres { get; set; }
        public IEnumerable<BookAuthor> BookAuthors { get; set; }
    }
}
