using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_Library.Core.Models;
using e_Library.DataAccess.InMemory;
using e_Library.Core.Contracts;

namespace e_Library.WebUI.Controllers
{
    [Authorize(Users = "21901959@dut4life.ac.za")]
    public class BookGenreManagerController : Controller
    {
        IRepository<BookGenre> context;

        public BookGenreManagerController(IRepository<BookGenre> context)
        {
            this.context = context;
        }
        // GET: BookGenreManager
        public ActionResult Index()
        {
            List<BookGenre> bookGenres = context.Collection().ToList();
            return View(bookGenres);
        }

        public ActionResult Create()
        {
            BookGenre bookGenre = new BookGenre();
            return View(bookGenre);
        }
        [HttpPost]
        public ActionResult Create(BookGenre bookGenre)
        {
            if (!ModelState.IsValid)
            {
                return View(bookGenre);
            }
            else
            {
                context.Insert(bookGenre);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            BookGenre bookGenre = context.Find(Id);
            if (bookGenre == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(bookGenre);
            }
        }
        [HttpPost]
        public ActionResult Edit(BookGenre bookGenre, string Id)
        {
            BookGenre bookGenreToEdit = context.Find(Id);
            if (bookGenreToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(bookGenre);
                }

                bookGenreToEdit.Genre = bookGenre.Genre;


                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            BookGenre bookGenreToDelete = context.Find(Id);

            if (bookGenreToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(bookGenreToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            BookGenre bookGenreToDelete = context.Find(Id);

            if (bookGenreToDelete == null)
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