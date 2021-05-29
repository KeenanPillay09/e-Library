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
    public class BookAuthorManagerController : Controller
    {
        IRepository<BookAuthor> context;

        public BookAuthorManagerController(IRepository<BookAuthor> context)
        {
            this.context = context;
        }
        // GET: BookAuthorManager
        public ActionResult Index()
        {
            List<BookAuthor> bookAuthors = context.Collection().ToList();
            return View(bookAuthors);
        }

        public ActionResult Create()
        {
            BookAuthor bookAuthor = new BookAuthor();
            return View(bookAuthor);
        }
        [HttpPost]
        public ActionResult Create(BookAuthor bookAuthor)
        {
            if (!ModelState.IsValid)
            {
                return View(bookAuthor);
            }
            else
            {
                context.Insert(bookAuthor);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            BookAuthor bookAuthor = context.Find(Id);
            if (bookAuthor == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(bookAuthor);
            }
        }
        [HttpPost]
        public ActionResult Edit(BookAuthor bookAuthor, string Id)
        {
            BookAuthor bookAuthorToEdit = context.Find(Id);
            if (bookAuthorToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(bookAuthor);
                }

                bookAuthorToEdit.Author = bookAuthor.Author;


                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            BookAuthor bookAuthorToDelete = context.Find(Id);

            if (bookAuthorToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(bookAuthorToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            BookAuthor bookAuthorToDelete = context.Find(Id);

            if (bookAuthorToDelete == null)
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