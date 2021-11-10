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
        IRepository<Book> Books;

        public PreOrderBookManagerController(IRepository<PreOrderBook> bookContext, IRepository<Book> Bookcontext, IRepository<BookGenre> bookGenreContext, IRepository<BookAuthor> bookAuthorContext) //Needs to inject Repositories from DI Container
        {
            context = bookContext;
            bookGenres = bookGenreContext;
            bookAuthors = bookAuthorContext;
            Books = Bookcontext;
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
                bookToEdit.ReleaseDate = book.ReleaseDate;

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

        //Release Pre-Order Books
        public ActionResult ViewPreOrderBookReleases()
        {
            List<PreOrderBook> books = context.Collection().OrderBy(p=>p.ReleaseDate).ToList();
            return View(books);
        }

        public ActionResult PublishBook(string Id)
        {
            PreOrderBook preOrderbook = context.Find(Id);

            Book book = new Book();

            book.Name = preOrderbook.Name;
            book.Author = preOrderbook.Author;
            book.Description = preOrderbook.Description;
            book.Genre = preOrderbook.Genre;
            book.Price = preOrderbook.Price;
            book.Stock = preOrderbook.Stock;

            return View(book);
        }
        [HttpPost]
        public ActionResult PublishBook(Book book, HttpPostedFileBase file)
        {
            string preOrderBookId = book.Id;

            //Remove from Pre-Order Books
            //PreOrderBook preOrderbook = context.Find(preOrderBookId);
            //context.Delete(preOrderBookId);
            //context.Commit();

            //Move to Books
            book.Id = Guid.NewGuid().ToString();

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

                Books.Insert(book);
                Books.Commit();

                return RedirectToAction("ViewPreOrderBookReleases");
            }
        }
    }
}