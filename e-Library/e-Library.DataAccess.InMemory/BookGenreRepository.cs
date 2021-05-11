using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using e_Library.Core.Models;

namespace e_Library.DataAccess.InMemory
{
    public class BookGenreRepository
    {

        ObjectCache cache = MemoryCache.Default;
        List<BookGenre> bookGenres;

        public BookGenreRepository()
        {
            bookGenres = cache["bookGenres"] as List<BookGenre>;
            if (bookGenres == null)
            {
                bookGenres = new List<BookGenre>();
            }
        }

        public void Commit()
        {
            cache["bookGenres"] = bookGenres;
        }

        public void Insert(BookGenre b)
        {
            bookGenres.Add(b);
        }

        public void Update(BookGenre bookGenre)
        {
            BookGenre bookGenreToUpdate = bookGenres.Find(b => b.Id == bookGenre.Id);

            if (bookGenreToUpdate != null)
            {
                bookGenreToUpdate = bookGenre;
            }
            else
            {
                throw new Exception("Genre not Found");
            }
        }


        public BookGenre Find(string Id)
        {
            BookGenre bookGenre = bookGenres.Find(b => b.Id == Id);

            if (bookGenre != null)
            {
                return bookGenre;
            }
            else
            {
                throw new Exception("Genre not Found");
            }
        }

        public IQueryable<BookGenre> Collection()
        {
            return bookGenres.AsQueryable();
        }

        public void Delete(string Id)
        {
            BookGenre bookGenreToDelete = bookGenres.Find(b => b.Id == Id);

            if (bookGenreToDelete != null)
            {
                bookGenres.Remove(bookGenreToDelete);
            }
            else
            {
                throw new Exception("Genre not Found");
            }
        }
    }
}
