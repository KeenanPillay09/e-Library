using e_Library.Core.Contracts;
using e_Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_Library.Core.ViewModels;
using System.IO;

namespace e_Library.WebUI.Controllers
{
    [Authorize(Users = "21901959@dut4life.ac.za")]
    public class PreBookManagerController : Controller
    {
        IRepository<PreBook> context;
        IRepository<BookGenre> bookGenres;
        IRepository<BookAuthor> bookAuthors;

        public PreBookManagerController(IRepository<PreBook> prebookContext, IRepository<BookGenre> bookGenreContext, IRepository<BookAuthor> bookAuthorContext) //Needs to inject Repositories from DI Container
        {
            context = prebookContext;
            bookGenres = bookGenreContext;
            bookAuthors = bookAuthorContext;
        }
        // GET: PreBookManager
        public ActionResult Index()
        {
            List<PreBook> books = context.Collection().ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            PreBookManagerViewModel viewModel = new PreBookManagerViewModel();

            viewModel.PreBook = new PreBook();
            viewModel.BookGenres = bookGenres.Collection();
            viewModel.BookAuthors = bookAuthors.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(PreBook book, HttpPostedFileBase file)
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
                    file.SaveAs(Server.MapPath("//Content//PreBookImages//") + book.Image);
                }
                context.Insert(book);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            PreBook book = context.Find(Id);
            if (book == null)
            {
                return HttpNotFound();
            }
            else
            {
                PreBookManagerViewModel viewModel = new PreBookManagerViewModel();
                viewModel.PreBook = book;
                viewModel.BookGenres = bookGenres.Collection();
                viewModel.BookAuthors = bookAuthors.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(PreBook book, string Id, HttpPostedFileBase file)
        {
            PreBook bookToEdit = context.Find(Id);
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
            PreBook bookToDelete = context.Find(Id);

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
            PreBook bookToDelete = context.Find(Id);

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