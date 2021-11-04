using e_Library.Core.Contracts;
using e_Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class DriverManagerController : Controller
    {
        IRepository<Driver> context;
        public DriverManagerController(IRepository<Driver> context)
        {
            this.context = context;
        }

        // GET: DriverManager
        public ActionResult Index()
        {
            List<Driver> drivers = context.Collection().ToList();
            return View(drivers);
        }

        public ActionResult Create()
        {
            Driver driver = new Driver();
            return View(driver);
        }
        [HttpPost]
        public ActionResult Create(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return View(driver);
            }
            else
            {
                context.Insert(driver);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Driver driver = context.Find(Id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(driver);
            }
        }
        [HttpPost]
        public ActionResult Edit(Driver driver, string Id)
        {
            Driver driverToEdit = context.Find(Id);
            if (driverToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(driver);
                }

                driverToEdit.DriverName = driver.DriverName;
                driverToEdit.Email = driver.Email;
                driverToEdit.City = driver.City;
                driverToEdit.Street = driver.Street;
                driverToEdit.Suburb = driver.Suburb;


                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            Driver driverToDelete = context.Find(Id);

            if (driverToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(driverToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Driver driverToDelete = context.Find(Id);

            if (driverToDelete == null)
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