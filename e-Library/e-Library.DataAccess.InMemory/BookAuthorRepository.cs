using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using e_Library.Core.Models;

namespace e_Library.DataAccess.InMemory
{
    public class BookAuthorRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<BookAuthor> bookAuthors;

        public BookAuthorRepository()
        {
            bookAuthors = cache["bookAuthors"] as List<BookAuthor>;
            if (bookAuthors == null)
            {
                bookAuthors = new List<BookAuthor>();
            }
        }

        public void Commit()
        {
            cache["bookAuthors"] = bookAuthors;
        }

        public void Insert(BookAuthor a)
        {
            bookAuthors.Add(a);
        }

        public void Update(BookAuthor bookAuthor)
        {
            BookAuthor bookAuthorToUpdate = bookAuthors.Find(b => b.Id == bookAuthor.Id);

            if (bookAuthorToUpdate != null)
            {
                bookAuthorToUpdate = bookAuthor;
            }
            else
            {
                throw new Exception("Author not Found");
            }
        }


        public BookAuthor Find(string Id)
        {
            BookAuthor bookAuthor = bookAuthors.Find(b => b.Id == Id);

            if (bookAuthor != null)
            {
                return bookAuthor;
            }
            else
            {
                throw new Exception("Author not Found");
            }
        }

        public IQueryable<BookAuthor> Collection()
        {
            return bookAuthors.AsQueryable();
        }

        public void Delete(string Id)
        {
            BookAuthor bookAuthorToDelete = bookAuthors.Find(b => b.Id == Id);

            if (bookAuthorToDelete != null)
            {
                bookAuthors.Remove(bookAuthorToDelete);
            }
            else
            {
                throw new Exception("Author not Found");
            }
        }
    }
}
