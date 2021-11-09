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
    public class PreOrderBasketController : Controller
    {

        IPreOrderService orderService;
        IRepository<Customer> customers;
        IRepository<PreOrderBook> preOrderBooks;

        public PreOrderBasketController(IPreOrderService OrderService, IRepository<Customer> customers, IRepository<PreOrderBook> preOrderBooks)
        {
            this.orderService = OrderService;
            this.customers = customers;
            this.preOrderBooks = preOrderBooks;
        }

        [Authorize]
        public ActionResult Checkout(string Id)
        {
            PreOrderBook book = new PreOrderBook();
            book = preOrderBooks.Find(Id);
        

            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                PreOrder order = new PreOrder()
                {
                    BookId = book.Id,
                    BookName = book.Name,
                    Email = customer.Email,
                    City = customer.City,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ZipCode = customer.ZipCode,
                    BasketTotal = book.Price
                };
                order.BasketTotal = book.Price;
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(PreOrder order)
        {
            order.Id = Guid.NewGuid().ToString();


            var book = preOrderBooks.Collection().Where(p => p.Name == order.BookName).FirstOrDefault();
            order.BookId = book.Id;

            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
            order.OrderStatus = "Delivery Required";
            order.Driver = "";

            //Populate Delivery Method from Form
            order.Delivery = PreOrder.deliveryType.Courier;
            order.DeliveryMethod = "Standard Delivery";
            order.DeliveryDate = book.ReleaseDate;

            //Determine Suburb
            order.Area = order.Area;
            order.Suburb = order.DetermineSuburb();

            //Create Order
            orderService.CreatePreOrder(order);

            return RedirectToAction("OrderSummary", new { Id = order.Id });
        }

        //Order Summary

        public ActionResult OrderSummary(string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);
            return View(order);
        }
        [HttpPost]
        public ActionResult OrderSummary(PreOrder objOrder)
        {
            return RedirectToAction("Payment", new { FinalTotal = objOrder.BasketTotal });
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
            string subject = "E-Library Pre-Order Confirmation  ";
            string message = "Hi " + fname + " We have received your pre-order and are processing it. See you soon!";

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

    }
}