using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_Library.Core.Models;
using e_Library.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class BookManagerController : Controller
    {
        BookRepository context;

        public BookManagerController()
        {
            context = new BookRepository();
        }
        // GET: BookManager
        public ActionResult Index()
        {
            List<Book> books = context.Collection().ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            Book book = new Book();
            return View(book);
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            else
            {
                context.Insert(book);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
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
        [HttpPost]
        public ActionResult Edit(Book book, string Id)
        {
            Book bookToEdit = context.Find(Id);
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

                bookToEdit.Name = book.Name;
                bookToEdit.Author = book.Author;
                bookToEdit.Genre = book.Genre;
                bookToEdit.Description = book.Description;
                bookToEdit.Image = book.Image;
                bookToEdit.Stock = book.Stock;
                bookToEdit.Price = book.Price;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            Book bookToDelete = context.Find(Id);

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
            Book bookToDelete = context.Find(Id);

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
    }
}