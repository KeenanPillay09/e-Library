﻿using e_Library.Core.Contracts;
using e_Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Library.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;
        IOrderService orderService;
        IRepository<Customer> customers;

        public BasketController(IBasketService BasketService, IOrderService OrderService, IRepository<Customer> customers)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = customers;
        }
        // GET: Basket2
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }

        [Authorize]
        public ActionResult Checkout(decimal basketTotal)
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ZipCode = customer.ZipCode,
                    BasketTotal = basketTotal
                };
                order.BasketTotal = basketTotal;
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {

            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;


            //delivery
            if (order.Delivery.ToString() == "Courier")
            {
                return RedirectToAction("Courier", order);
            }
            else
            if (order.Delivery.ToString() == "Collect")
            {
                return RedirectToAction("Collect", order);
            }

            //process payment
            // order.OrderStatus = "Payment Processed";
            return RedirectToAction("ThankYou", new { OrderId = order.Id });
        }

        public ActionResult Courier()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Courier(Order objOrder)
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            var basketItems = basketService.GetBasketItems(this.HttpContext);

            //Populate Order with Customer Details
            objOrder.Email = customer.Email;
            objOrder.City = customer.City;
            objOrder.Street = customer.Street;
            objOrder.FirstName = customer.FirstName;
            objOrder.LastName = customer.LastName;
            objOrder.ZipCode = customer.ZipCode;
            objOrder.OrderStatus = "Delivery Required";

            //Populate Delivery Method from Form
            objOrder.DeliveryMethod = objOrder.DeliveryMethod;
            //Get Basket Total from Method in Basket Service
            objOrder.BasketTotal = basketService.BasketTotal(this.HttpContext);
            //Calculate Final Total from Method in Model
            objOrder.FinalTotal = objOrder.CalcOrderFinalTotal();
            //Create Order
            orderService.CreateOrder(objOrder, basketItems);
            //Clear Basket
            basketService.ClearBasket(this.HttpContext);

            //Sending Final Total to Payment Page
            return RedirectToAction("Payment", new { FinalTotal = objOrder.FinalTotal });
            //return View("Courier", objOrder);
        }
        public ActionResult Collect()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Collect(Order objOrder)
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            var basketItems = basketService.GetBasketItems(this.HttpContext);

            //Populate Order with Customer Details
            objOrder.Email = customer.Email;
            objOrder.City = customer.City;
            objOrder.Street = customer.Street;
            objOrder.FirstName = customer.FirstName;
            objOrder.LastName = customer.LastName;
            objOrder.ZipCode = customer.ZipCode;
            objOrder.OrderStatus = "Delivery Required";

            //Populate Delivery Method from Form
            objOrder.DeliveryMethod = objOrder.DeliveryMethod;
            //Get Basket Total from Method in Basket Service
            objOrder.BasketTotal = basketService.BasketTotal(this.HttpContext);
            //Calculate Final Total from Method in Model
            objOrder.FinalTotal = objOrder.CalcOrderFinalTotal();
            //Create Order
            orderService.CreateOrder(objOrder, basketItems);
            //Clear Basket
            basketService.ClearBasket(this.HttpContext);

            //Sending Final Total to Payment Page
            return RedirectToAction("Payment", new { FinalTotal = objOrder.FinalTotal });
            //return View("Collect", objOrder);
        }

        public ActionResult Payment(decimal FinalTotal)
        {
            string url = "";
            decimal fTotal = FinalTotal;
            
            
            fTotal = Decimal.Ceiling(fTotal);
            url = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&amount=" + (fTotal) + "&business=JanjuaTailors@Shop.com&item_name=Books&return=/Basket/ThankYou";

            return Redirect(url);
        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}