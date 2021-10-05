using e_Library.Core.Contracts;
using e_Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Library.WebUI.Controllers
{
    public class ChartController : Controller
    {
        IRepository<Book> books;

        public ChartController(IRepository<Book> booksContext)
        {
            books = booksContext;
        }


        public ActionResult ChartOptions()
        {
            return View();
        }

        // GET: Chart
        public ActionResult Index()
        {
            List<Book> book = books.Collection().ToList();
            return View(book);
        }

        public ActionResult DisplayChart()
        {
            List<Book> book = books.Collection().ToList();
            return View(book);
        }
    }
}