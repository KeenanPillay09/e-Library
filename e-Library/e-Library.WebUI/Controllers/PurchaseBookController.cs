using e_Library.Core.Contracts;
using e_Library.Core.Models;
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
        public ActionResult Index()
        {
            List<Book> books = context.Collection().ToList();
            return View(books);
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