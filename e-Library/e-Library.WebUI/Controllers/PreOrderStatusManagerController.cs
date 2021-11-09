using e_Library.Core.Contracts;
using e_Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class PreOrderStatusManagerController : Controller
    {
        IRepository<PreOrderStatusModel> context;
        public PreOrderStatusManagerController(IRepository<PreOrderStatusModel> context)
        {
            this.context = context;
        }

        // GET: OrderStatusManager
        public ActionResult Index()
        {
            List<PreOrderStatusModel> orderStatuses = context.Collection().ToList();
            return View(orderStatuses);
        }

        public ActionResult Create()
        {
            PreOrderStatusModel orderStatuses = new PreOrderStatusModel();
            return View(orderStatuses);
        }
        [HttpPost]
        public ActionResult Create(PreOrderStatusModel orderStatuses)
        {
            if (!ModelState.IsValid)
            {
                return View(orderStatuses);
            }
            else
            {
                context.Insert(orderStatuses);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            PreOrderStatusModel orderStatuses = context.Find(Id);
            if (orderStatuses == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(orderStatuses);
            }
        }
        [HttpPost]
        public ActionResult Edit(PreOrderStatusModel orderStatuses, string Id)
        {
            PreOrderStatusModel orderStatusesToEdit = context.Find(Id);
            if (orderStatusesToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(orderStatuses);
                }

                orderStatusesToEdit.OrderStatus = orderStatuses.OrderStatus;


                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string Id)
        {
            PreOrderStatusModel orderStatusesToDelete = context.Find(Id);

            if (orderStatusesToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(orderStatusesToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            PreOrderStatusModel orderStatusesToDelete = context.Find(Id);

            if (orderStatusesToDelete == null)
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