﻿using e_Library.Core.Contracts;
using e_Library.Core.Models;
using e_Library.Core.ViewModels;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace e_Library.WebUI.Controllers
{

    public class PreOrderManagerController : Controller
    {
        IPreOrderService orderService;
        IRepository<Driver> drivers;
        IRepository<PreOrderStatusModel> orderStatuses;

        public PreOrderManagerController(IPreOrderService OrderService, IRepository<Driver> driversContext, IRepository<PreOrderStatusModel> orderStatusContext)
        {
            this.orderService = OrderService;
            drivers = driversContext;
            orderStatuses = orderStatusContext;

        }
        // GET: OrderManager
        [Authorize(Users = "21901959@dut4life.ac.za")]
        public ActionResult Index()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();

            return View(orders);
        }

        public ActionResult DisplayOrdersWithFilter(string Status = null)
        {
            List<PreOrder> orders;
            List<PreOrderStatusModel> statuses = orderStatuses.Collection().ToList();

            if (Status == null)
            {
                orders = orderService.GetPreOrderList().Where(p => p.OrderStatus != "Order Complete").ToList();
            }
            else
            {
                orders = orderService.GetPreOrderList().Where(p => p.OrderStatus == Status).ToList();
            }

            PreOrderStatusListViewModel model = new PreOrderStatusListViewModel();
            model.Orders = orders;
            model.OrderStatusModels = statuses;

            return View(model);
        }


        //View Driver Deliveries
        public ActionResult DriverDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => (p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && (p.OrderStatus != "Order Complete"))).ToList();

            return View(orders);
        }
        //Display Driver Suburbs with Orders 
        //1-North
        public ActionResult NorthDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => (p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && (p.OrderStatus != "Order Complete") && (p.Suburb == "North"))).ToList();

            return View(orders);
        }
        //2-Central
        public ActionResult CentralDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && p.OrderStatus == "Order Ready" && p.Suburb == "Central").ToList();

            return View(orders);
        }
        //3-South
        public ActionResult SouthDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && p.OrderStatus == "Order Ready" && p.Suburb == "South").ToList();

            return View(orders);
        }
        //4-Outer West
        public ActionResult OuterWestDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && p.OrderStatus == "Order Ready" && p.Suburb == "Outer West").ToList();

            return View(orders);
        }
        //5-Inner West
        public ActionResult InnerWestDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && p.OrderStatus == "Order Ready" && p.Suburb == "Inner West").ToList();

            return View(orders);
        }
        //Prepare Delivery Orders
        public ActionResult PrepareOrderForDelivery()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.OrderStatus == "Delivery Required" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery")).ToList();
            return View(orders);
        }

        //Prepare Order for Delivery
        public ActionResult PrepareDelivery(string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);
            order.Drivers = drivers.Collection();

            return View(order);
        }
        [HttpPost]
        public ActionResult PrepareDelivery(PreOrder preparedOrder, string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);
            //Get all drivers
            order.Drivers = drivers.Collection();
            string orderSuburb = order.Suburb.ToString(); //Get Order Suburb

            if (orderSuburb == "Inner West")
            {
                orderSuburb = "Inner_West";
            }
            else
            if (orderSuburb == "Outer West")
            {
                orderSuburb = "Outer_West";
            }


            var findDriverAllocatedToSuburb = order.Drivers.Where(d => d.Suburb.ToString() == orderSuburb).FirstOrDefault(); //Get Driver Allocated to Suburb

            if (findDriverAllocatedToSuburb != null)
            {
                order.Driver = findDriverAllocatedToSuburb.DriverName; //Assign Driver Automatically             
            }


            //order.Driver = preparedOrder.Driver; //Not taking the admins manual assignment. Its automatically done above

            //Generate and Save QR Code
            string qrcode = "https://localhost:44317/DriverPortal/UpdateOrder/" + Id;

            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qRCodeData);

                using (Bitmap bitmap = qrCode.GetGraphic(20))
                {
                    // bitmap.Save(ms, ImageFormat.Png);
                    order.QRCode = order.Id + ".png";
                    bitmap.Save(Server.MapPath("//Content//QRCodes//") + order.QRCode);
                }
            }

            //Send Email to Customer to notify of order progression

            string customer = order.Email; //Returns the customers email
            string fname = order.FirstName; //Returns the customers first name


            string receiver = customer;
            string subject = "E-Library Pre-Order Delivery Details  ";
            string message = "Hi " + fname + " We are currently processing your order.The order will be delivered on the " + (order.DeliveryDate) + " See you soon!";

            try
            {
                //if (ModelState.IsValid)
                //{
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
                    Body = body,
                })
                {
                    smtp.Send(mess);
                }
                //  return View();
                //}
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }

            //Send Email to Driver to notify them about delivery

            DateTime? delDate = (order.DeliveryDate);



            string driver = "peakydriver51@gmail.com"; //Returns the drivers email
            string fname2 = order.FirstName; //Returns the customers first name


            string receiver2 = driver;
            string subject2 = "E-Library Pre-Order Delivery Details";
            string body2 = "Hi " + order.Driver + "<br/>The following pre-order needs to be delivered on the " + delDate + "<br/>The delivery details are below:<br/>" +
                "Delivery Address: " + order.Street + "<br/>" +
                "Customer Name: " + order.FirstName + " " + order.LastName + "<br/>" +
                "Order ID: " + order.Id;

            MailMessage mm = new MailMessage();

            mm.From = new MailAddress("peakylibrary@outlook.com");
            mm.To.Add(receiver2);
            mm.Subject = subject2;


            AlternateView imgView = AlternateView.CreateAlternateViewFromString(body2 + "<br/><img src=cId:imgpath height=150 width=150>", null, "text/html");
            LinkedResource lr = new LinkedResource("C:/Users/User/source/repos/e-Library/e-Library/e-Library.WebUI/Content/QRCodes/" + order.QRCode);
            lr.ContentId = "imgpath";
            imgView.LinkedResources.Add(lr);
            mm.AlternateViews.Add(imgView);
            mm.Body = lr.ContentId;
            SmtpClient smtp2 = new SmtpClient();
            smtp2.Host = "smtp-mail.outlook.com";
            smtp2.Port = 587;
            NetworkCredential nc = new NetworkCredential("peakylibrary@outlook.com", "Ballantines2021");
            smtp2.EnableSsl = true;
            smtp2.Credentials = nc;
            smtp2.Send(mm);

            order.OrderStatus = "Order Ready";
            orderService.UpdatePreOrder(order);

            return RedirectToAction("PrepareOrderForDelivery");
        }


        //Update Order

        [Authorize(Users = "21901959@dut4life.ac.za")]
        public ActionResult UpdateOrder(string Id)
        {
            ViewBag.StatusList = new List<string>() {
                "Order Created",
                "Payment Processed",
                "Delivery Required",
                "Out for Delivery",
                "Order Complete"
            };

            PreOrder order = orderService.GetPreOrder(Id);
            order.Drivers = drivers.Collection();

            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrder(PreOrder updatedOrder, string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);

            order.OrderStatus = updatedOrder.OrderStatus;
            order.Driver = updatedOrder.Driver;
            orderService.UpdatePreOrder(order);

            return RedirectToAction("DisplayOrdersWithFilter");
        }

        //Delivery Delays
        public ActionResult DelayOrder(string Id)
        {
            ViewBag.StatusList = new List<string>() {
                "Order Created",
                "Payment Processed",
                "Delivery Required",
                "Out for Delivery",
                "Order Complete"
            };

            PreOrder order = orderService.GetPreOrder(Id);
            order.Drivers = drivers.Collection();
            order.DeliveryDate = order.DeliveryDate;

            return View(order);
        }

        [HttpPost]
        public ActionResult DelayOrder(PreOrder updatedOrder, string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);

            //order.OrderStatus = updatedOrder.OrderStatus;
            //order.Driver = updatedOrder.Driver;
            order.DeliveryDate = updatedOrder.DeliveryDate;

            //Send Email to Customer notifying them of the delay
            string customer = order.Email; //Returns the customers email
            string fname = order.FirstName; //Returns the customers first name


            string receiver = customer;
            string subject = "E-Library Order Delivery Delay  ";
            string message = "Hi " + fname + " Unfortunately we regret to inform you that the delivery of your order has been delayed due to unforseen circumstances.The order will now be delivered on the " + (order.DeliveryDate) + " We apologise for any inconvenience caused by the delay!";

            try
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
                    Body = body,
                })
                {
                    smtp.Send(mess);
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }


            orderService.UpdatePreOrder(order);

            return RedirectToAction("DriverDeliveries");
        }

        public ActionResult CollectionDelay(string Id)
        {
            ViewBag.StatusList = new List<string>() {
                "Order Created",
                "Payment Processed",
                "Delivery Required",
                "Out for Delivery",
                "Order Complete"
            };

            PreOrder order = orderService.GetPreOrder(Id);

            order.DeliveryDate = order.DeliveryDate;

            return View(order);
        }

        [HttpPost]
        public ActionResult CollectionDelay(PreOrder updatedOrder, string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);

            //order.OrderStatus = updatedOrder.OrderStatus;
            //order.Driver = updatedOrder.Driver;
            order.DeliveryDate = updatedOrder.DeliveryDate;

            //Send Email to Customer notifying them of the delay
            string customer = order.Email; //Returns the customers email
            string fname = order.FirstName; //Returns the customers first name


            string receiver = customer;
            string subject = "E-Library Order Delivery Delay  ";
            string message = "Hi " + fname + " Your order has not been collected on the given date.The order can now be collected on the " + (order.DeliveryDate) + " See you soon!";

            try
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
                    Body = body,
                })
                {
                    smtp.Send(mess);
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }


            orderService.UpdatePreOrder(order);

            return RedirectToAction("DriverDeliveries");
        }



        //Driver Portal
        public ActionResult DriverPortal(string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);
            order.OrderStatus = "Order Complete";
            orderService.UpdatePreOrder(order);

            return View(order);
        }
        //[HttpPost]
        //public ActionResult DriverPortal(Order updatedOrder, string Id)
        //{
        //    //Updating Order Status
        //    Order order = orderService.GetOrder(Id);

        //    order.OrderStatus = updatedOrder.OrderStatus;

        //    orderService.UpdateOrder(order);


        //    return RedirectToAction("Index");
        //}
        //Collection Portal
        public ActionResult CollectionPortal(string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);
            order.OrderStatus = "Order Complete";
            orderService.UpdatePreOrder(order);

            return View(order);
        }

        //Generating QR Code
        public ActionResult GenerateQRCode(string id)
        {
            ViewBag.qrcode = "https://localhost:44317/DriverPortal/UpdateOrder/" + id;
            // ViewBag.qrcode = "https://2021grp09.azurewebsites.net/OrderManager/DriverPortal/" + id;

            return View();
        }
        [HttpPost]
        public ActionResult QRCode(string qrcode)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qRCodeData);

                using (Bitmap bitmap = qrCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            return View("GenerateQRCode");
        }


        [Authorize(Users = "21901959@dut4life.ac.za")]
        public ActionResult ViewAnalytics()
        {
            string url = "";

            url = "https://analytics.google.com/analytics/web/?hl=en&pli=1#/p286465082/reports/reportinghub";                                                                                                                                                                                     // url = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&amount=" + (fTotal) + "&business=JanjuaTailors@Shop.com&item_name=Books&return=https://2021grp09.azurewebsites.net/Basket/ThankYou"; //deploy

            return Redirect(url);
        }

        //Customer Portal

        public ActionResult ViewAllOrders(string Email)
        {
            List<PreOrder> orders;
            orders = orderService.GetPreOrderList().Where(p => p.Email == Email).ToList();
            return View(orders);
        }

        //Return
        public ActionResult ReturnOrder(string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);

            return View(order);
        }
    }
}