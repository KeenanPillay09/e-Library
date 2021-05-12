using e_Library.Core.Contracts;
using e_Library.Core.Models;
using e_Library.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Library.WebUI.Controllers
{
    public class PurchaseBookController : Controller
    {
        IRepository<Book> context;
        IRepository<BookGenre> bookGenres;
        IRepository<BookAuthor> bookAuthors;

        public PurchaseBookController(IRepository<Book> bookContext, IRepository<BookGenre> bookGenreContext, IRepository<BookAuthor> bookAuthorContext) //Needs to inject Repositories from DI Container
        {
            context = bookContext;
            bookGenres = bookGenreContext;
            bookAuthors = bookAuthorContext;
        }
        // GET: PurchaseBook
        public ActionResult Index(string Genre=null)
        {
            List<Book> books;
            List<BookGenre> genres = bookGenres.Collection().ToList();

            if (Genre == null)
            {
                books = context.Collection().ToList();
            }
            else
            {
                books = context.Collection().Where(p => p.Genre == Genre).ToList();
            }

            BookListViewModel model = new BookListViewModel();
            model.Books = books;
            model.BookGenres = genres;

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Book book = context.Find(Id);
            if (book == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(book);
            }
        }
    }
}