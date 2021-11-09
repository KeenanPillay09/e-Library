using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_Library.Core.Models;
using e_Library.Core.ViewModels;
using e_Library.DataAccess.InMemory;
using e_Library.Core.Contracts;
using System.IO;
using System.Data;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Users = "21901959@dut4life.ac.za")]
    public class PreOrderBookManagerController : Controller
    {
        IRepository<PreOrderBook> context;
        IRepository<BookGenre> bookGenres;
        IRepository<BookAuthor> bookAuthors;

        public PreOrderBookManagerController(IRepository<PreOrderBook> bookContext, IRepository<BookGenre> bookGenreContext, IRepository<BookAuthor> bookAuthorContext) //Needs to inject Repositories from DI Container
        {
            context = bookContext;
            bookGenres = bookGenreContext;
            bookAuthors = bookAuthorContext;
        }
        // GET: BookManager
        public ActionResult Index()
        {
            List<PreOrderBook> books = context.Collection().ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            PreOrderBookManagerViewModel viewModel = new PreOrderBookManagerViewModel();

            viewModel.Book = new PreOrderBook();
            viewModel.BookGenres = bookGenres.Collection();
            viewModel.BookAuthors = bookAuthors.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(PreOrderBook book, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            else
            {
                if (file != null)
                {
                    book.Image = book.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//BookImages//") + book.Image);
                }
                context.Insert(book);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            PreOrderBook book = context.Find(Id);
            if (book == null)
            {
                return HttpNotFound();
            }
            else
            {
                PreOrderBookManagerViewModel viewModel = new PreOrderBookManagerViewModel();
                viewModel.Book = book;
                viewModel.BookGenres = bookGenres.Collection();
                viewModel.BookAuthors = bookAuthors.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(PreOrderBook book, string Id, HttpPostedFileBase file)
        {
            PreOrderBook bookToEdit = context.Find(Id);
            if (bookToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(book);
                }

                if (file != null)
                {
                    bookToEdit.Image = book.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//BookImages//") + bookToEdit.Image);
                }

                bookToEdit.Name = book.Name;
                bookToEdit.Author = book.Author;
                bookToEdit.Genre = book.Genre;
                bookToEdit.Description = book.Description;
                bookToEdit.Stock = book.Stock;
                bookToEdit.Price = book.Price;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            PreOrderBook bookToDelete = context.Find(Id);

            if (bookToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(bookToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            PreOrderBook bookToDelete = context.Find(Id);

            if (bookToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Dashboard()
        {
            List<PreOrderBook> list = context.Collection().Where(p => p.Stock > 0).ToList();
            List<int> repartitons = new List<int>();

            var totalsales = context.Collection().OrderBy(p => p.Name).OrderByDescending(p => p.Stock).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", Type.GetType("System.String"));
            dt.Columns.Add("Stock", System.Type.GetType("System.Int32"));

            int iCnt = 0;
            foreach (PreOrderBook book in totalsales)
            {
                //for (int i = 0; i < 5; i++)
                //{
                DataRow dr = dt.NewRow();
                dr["BookName"] = book.Name;
                dr["Stock"] = book.Stock;
                dt.Rows.Add(dr);
                iCnt++;
                if (iCnt == 5)
                {
                    break;
                }
                //}
                //bookName = "";
                //stock = 0;
            }

            List<object> iData = new List<object>();
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            ViewBag.ChartData = iData;

            return View();

        }
    }
}