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
    public class PreOrderBookController : Controller
    {
        IRepository<PreOrderBook> context;
        IRepository<BookGenre> bookGenres;
        IRepository<BookAuthor> bookAuthors;

        public PreOrderBookController(IRepository<PreOrderBook> bookContext, IRepository<BookGenre> bookGenreContext, IRepository<BookAuthor> bookAuthorContext) //Needs to inject Repositories from DI Container
        {
            context = bookContext;
            bookGenres = bookGenreContext;
            bookAuthors = bookAuthorContext;
        }
        // GET: PreOrderBook
        public ActionResult Index(string Genre = null)
        {
            List<PreOrderBook> books;
            List<BookGenre> genres = bookGenres.Collection().ToList();

            if (Genre == null)
            {
                books = context.Collection().ToList();
            }
            else
            {
                books = context.Collection().Where(p => p.Genre == Genre).ToList();
            }

            PreOrderBookListViewModel model = new PreOrderBookListViewModel();
            model.Books = books;
            model.BookGenres = genres;

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            PreOrderBook book = context.Find(Id);
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