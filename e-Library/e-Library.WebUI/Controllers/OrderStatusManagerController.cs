using e_Library.Core.Contracts;
using e_Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class OrderStatusManagerController : Controller
    {
        IRepository<OrderStatusModel> context;
        public OrderStatusManagerController(IRepository<OrderStatusModel> context)
        {
            this.context = context;
        }

        // GET: OrderStatusManager
        public ActionResult Index()
        {
            List<OrderStatusModel> orderStatuses = context.Collection().ToList();
            return View(orderStatuses);
        }

        public ActionResult Create()
        {
            OrderStatusModel orderStatuses = new OrderStatusModel();
            return View(orderStatuses);
        }
        [HttpPost]
        public ActionResult Create(OrderStatusModel orderStatuses)
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
            OrderStatusModel orderStatuses = context.Find(Id);
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
        public ActionResult Edit(OrderStatusModel orderStatuses, string Id)
        {
            OrderStatusModel orderStatusesToEdit = context.Find(Id);
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
            OrderStatusModel orderStatusesToDelete = context.Find(Id);

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
            OrderStatusModel orderStatusesToDelete = context.Find(Id);

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