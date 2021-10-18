using e_Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Core.ViewModels
{
    public class PreBookManagerViewModel
    {
        public PreBook PreBook { get; set; }
        public IEnumerable<BookGenre> BookGenres { get; set; }
        public IEnumerable<BookAuthor> BookAuthors { get; set; }
    }
}
