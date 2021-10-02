using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_Library.Core.Contracts;
using e_Library.Core.Models;
using e_Library.Core.ViewModels;

namespace e_Library.WebUI.Controllers
{
    public class PreOrderManagerController : Controller
    {
        IRepository<PreOrderBooks> context;

        public PreOrderManagerController(IRepository<PreOrderBooks> bookContext)
        {
            context = bookContext;
        }


        // GET: PreOrderManager

        public ActionResult DisplayPreOrders()
        {
            List<PreOrderBooks> books;

            books = context.Collection().ToList();

            PreOrderListView model = new PreOrderListView();
            model.PreOrderBooks = books;

            return View(model);
        }

        public ActionResult ContactForm()
        {
            return View();
        }

        public ActionResult Details(string Id)
        {
            PreOrderBooks book = context.Find(Id);
            if (book == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(book);
            }
        }

        public ActionResult Index()
        {
            List<PreOrderBooks> books = context.Collection().ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            PreOrderBooks viewModel = new PreOrderBooks();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(PreOrderBooks book, HttpPostedFileBase file)
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
            PreOrderBooks book = context.Find(Id);
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
        public ActionResult Edit(PreOrderBooks book, string Id, HttpPostedFileBase file)
        {
            PreOrderBooks bookToEdit = context.Find(Id);
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

                bookToEdit.BookName = book.BookName;
                bookToEdit.Author = book.Author;
                bookToEdit.Category = book.Category;
                bookToEdit.Description = book.Description;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            PreOrderBooks bookToDelete = context.Find(Id);

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
            PreOrderBooks bookToDelete = context.Find(Id);

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