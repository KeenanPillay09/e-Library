using e_Library.Core.Contracts;
using e_Library.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
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
        [Authorize]
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("Index", "PurchaseBook");
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
            //order.Area = order.Area;
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



            objOrder.Driver = "";

            //Populate Delivery Method from Form
            objOrder.Delivery = Order.DeliveryType.Courier;
            objOrder.DeliveryMethod = objOrder.DeliveryMethod;
            objOrder.DeliveryDate = objOrder.CalcDeliveryDate();

            //Determine Suburb
            objOrder.Area = objOrder.Area;
            objOrder.Suburb = objOrder.DetermineSuburb();


            //Get Basket Total from Method in Basket Service
            objOrder.BasketTotal = basketService.BasketTotal(this.HttpContext);
            //Calculate Final Total from Method in Model
            objOrder.FinalTotal = objOrder.CalcOrderFinalTotal();
            //Create Order
            orderService.CreateOrder(objOrder, basketItems);
            //Clear Basket
            basketService.ClearBasket(this.HttpContext);

            //Sending Final Total to Payment Page
           //  return RedirectToAction("Payment", new { FinalTotal = objOrder.FinalTotal });

            //Sending Order Info to OrderSummary Page
            return RedirectToAction("OrderSummary", new { Id = objOrder.Id });

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
            objOrder.OrderStatus = "Pending Collection";
            objOrder.Driver = "No Driver Required";

            //Populate Delivery Method and determine Delivery Date from Form
            objOrder.Delivery = Order.DeliveryType.Collect;
            objOrder.DeliveryMethod = objOrder.DeliveryMethod;
            objOrder.DeliveryDate = objOrder.CalcDeliveryDate();

            //Determine Suburb
            //objOrder.Area = objOrder.Area;
            //objOrder.Suburb = objOrder.DetermineSuburb();

            //Get Basket Total from Method in Basket Service
            objOrder.BasketTotal = basketService.BasketTotal(this.HttpContext);
            //Calculate Final Total from Method in Model
            objOrder.FinalTotal = objOrder.CalcOrderFinalTotal();
            //Create Order
            orderService.CreateOrder(objOrder, basketItems);
            //Clear Basket
            basketService.ClearBasket(this.HttpContext);

            //Sending Final Total to Payment Page
            //  return RedirectToAction("Payment", new { FinalTotal = objOrder.FinalTotal });
         

            //Sending Order Info to OrderSummary Page
            return RedirectToAction("OrderSummary", new { Id = objOrder.Id });
        }

        //Order Summary
      
        public ActionResult OrderSummary(string Id)
        {
            Order order = orderService.GetOrder(Id);
            Session["OrderID"] = Id;
            return View(order);
        }
        [HttpPost]
        public ActionResult OrderSummary(Order objOrder)
        {
            return RedirectToAction("Payment", new { FinalTotal = objOrder.FinalTotal});
        }



        public ActionResult Payment(decimal FinalTotal)
        {
            string url = "";
            decimal fTotal = FinalTotal;
            
            fTotal = Decimal.Ceiling(fTotal);
                url = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&amount=" + (fTotal) + "&business=peakylibrary@outlook.com&item_name=Books&return=https://localhost:44349/Basket/ThankYou/"; //localhost
               // url = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&amount=" + (fTotal) + "&business=JanjuaTailors@Shop.com&item_name=Books&return=https://2021grp09.azurewebsites.net/Basket/ThankYou"; //deploy

            return Redirect(url);
        }



        public ActionResult ThankYou() 
        {
            //Get Customer Details
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name); //Returns the user
            string fname = customer.FirstName; //name

            //Get Order Details
            

            string receiver = customer.Email;
            string subject = "E-Library Order Confirmation  ";
            string message ="Hi "+ fname+" We have received your order and are processing it. See you soon!";

            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("peakylibrary@outlook.com", "e-Library");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Ballantines2021";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp-mail.outlook.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }

            return View();
        }


        public ActionResult TestEmail()
        {
            //string receiver = "21901959@dut4life.ac.za";
            //string subject = "Email Works";
            //string message = "We have received your order and are processing it. See you soon!";

            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        var senderEmail = new MailAddress("ballantines.pharmacy@gmail.com", "e-Library");
            //        var receiverEmail = new MailAddress(receiver, "Receiver");
            //        var password = "Ballantines2020";
            //        var sub = subject;
            //        var body = message;
            //        var smtp = new SmtpClient
            //        {
            //            Host = "smtp.gmail.com",
            //            Port = 587,
            //            EnableSsl = true,
            //            DeliveryMethod = SmtpDeliveryMethod.Network,
            //            UseDefaultCredentials = false,
            //            Credentials = new NetworkCredential(senderEmail.Address, password)
            //        };
            //        using (var mess = new MailMessage(senderEmail, receiverEmail)
            //        {
            //            Subject = subject,
            //            Body = body
            //        })
            //        {
            //            smtp.Send(mess);
            //        }
            //        return View();
            //    }
            //}
            //catch (Exception)
            //{
            //    ViewBag.Error = "Some Error";
            //}

            return View();

        }
    }
}