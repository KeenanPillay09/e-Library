using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching; //
using e_Library.Core.Models; //

namespace e_Library.DataAccess.InMemory
{
    public class BookRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Book> books;

        public BookRepository()
        {
            books = cache["books"] as List<Book>;
            if (books == null)
            {
                books = new List<Book>();
            }
        }

        public void Commit()
        {
            cache["books"] = books;
        }

        public void Insert(Book b)
        {
            books.Add(b);
        }

        public void Update(Book book)
        {
            Book bookToUpdate = books.Find(b => b.Id == book.Id);

            if (bookToUpdate != null)
            {
                bookToUpdate = book;
            }
            else
            {
                throw new Exception("Book not Found");
            }
        }


        public Book Find(string Id)
        {
            Book book = books.Find(b => b.Id == Id);

            if (book != null)
            {
                return book;
            }
            else
            {
                throw new Exception("Book not Found");
            }
        }

        public IQueryable<Book> Collection()
        {
            return books.AsQueryable();
        }

        public void Delete(string Id)
        {
            Book bookToDelete = books.Find(b => b.Id == Id);

            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
            }
            else
            {
                throw new Exception("Book not Found");
            }
        }
    }
}
