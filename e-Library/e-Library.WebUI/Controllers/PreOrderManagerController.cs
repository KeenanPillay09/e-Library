using e_Library.Core.Contracts;
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
                orders = orderService.GetPreOrderList().Where(p => p.OrderStatus != "Order Complete").OrderByDescending(p => p.CreatedAt).ToList();
            }
            else
            {
                orders = orderService.GetPreOrderList().Where(p => p.OrderStatus == Status).OrderByDescending(p => p.CreatedAt).ToList();
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
            orders = orderService.GetPreOrderList().Where(p => (p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && (p.OrderStatus != "Order Complete"))).OrderBy(p => p.DeliveryDate).ToList();

            return View(orders);
        }
        //Display Driver Suburbs with Orders 
        //1-North
        public ActionResult NorthDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => (p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && (p.OrderStatus != "Order Complete") && (p.Suburb == "North"))).OrderBy(p => p.DeliveryDate).ToList();

            return View(orders);
        }
        //2-Central
        public ActionResult CentralDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && p.OrderStatus == "Order Ready" && p.Suburb == "Central").OrderBy(p => p.DeliveryDate).ToList();

            return View(orders);
        }
        //3-South
        public ActionResult SouthDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && p.OrderStatus == "Order Ready" && p.Suburb == "South").OrderBy(p => p.DeliveryDate).ToList();

            return View(orders);
        }
        //4-Outer West
        public ActionResult OuterWestDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && p.OrderStatus == "Order Ready" && p.Suburb == "Outer West").OrderBy(p => p.DeliveryDate).ToList();

            return View(orders);
        }
        //5-Inner West
        public ActionResult InnerWestDeliveries()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.Driver != "" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery") && p.OrderStatus == "Order Ready" && p.Suburb == "Inner West").OrderBy(p => p.DeliveryDate).ToList();

            return View(orders);
        }
        //Prepare Delivery Orders
        public ActionResult PrepareOrderForDelivery()
        {
            List<PreOrder> orders = orderService.GetPreOrderList();
            orders = orderService.GetPreOrderList().Where(p => p.OrderStatus == "Delivery Required" && (p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery")).OrderBy(p => p.DeliveryDate).ToList();
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
            //deploy string qrcode = "https://2021grp09.azurewebsites.net/PreOrderManager/DriverPortal/UpdateOrder/" + Id;

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


        //Driver Portal
        public ActionResult DriverPortal(string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);
            order.OrderStatus = "Order Complete";
            orderService.UpdatePreOrder(order);

            return View(order);
        }

        public ActionResult ViewAllOrders(string Email)
        {
            List<PreOrder> orders;
            orders = orderService.GetPreOrderList().Where(p => p.Email == Email).OrderByDescending(p => p.CreatedAt).ToList();
            return View(orders);
        }

        //Return
        public ActionResult ReturnOrder(string Id)
        {
            PreOrder order = orderService.GetPreOrder(Id);

            PreOrderReturn returns = new PreOrderReturn();

            returns.CustomerName = order.FirstName + " " + order.LastName;
            returns.Email = order.Email;
            returns.OrderID = order.Id;


            return View(returns);
        }
        [HttpPost]
        public ActionResult ReturnOrder(PreOrderReturn returns)
        {
            returns.Id = null;
            returns.Status = "Pending";

            PreOrderReturn returnedOrder = new PreOrderReturn();

            returnedOrder.OrderID = returns.OrderID;
            returnedOrder.CustomerName = returns.CustomerName;
            returnedOrder.Email = returns.Email;
            returnedOrder.Reason = returns.Reason;
            returnedOrder.RefundType = returns.RefundType;
            returnedOrder.Status = returns.Status;

            orderService.CreateReturn(returnedOrder);

            return View("ReturnConfirmationPage");
        }
        public ActionResult ReturnConfirmationPage()
        {
            return View();
        }
        //Admin

        public ActionResult DisplayReturns(string Status = null)
        {
            List<PreOrderReturn> returns;

            if (Status == null)
            {
                returns = orderService.GetReturnList().Where(p => p.Status == "Pending").OrderBy(p => p.CreatedAt).ToList();
            }
            else
            {
                returns = orderService.GetReturnList().Where(p => p.Status == Status).OrderBy(p => p.CreatedAt).ToList();
            }
            return View(returns);
        }
        public ActionResult ApprovedReturns(string Status = null)
        {
            //List<Return> returns = orderService.GetReturnList().Where(p => p.Status == "Approved").ToList();
            //return View(returns);
            List<PreOrderReturn> returns;

            if (Status == null)
            {
                returns = orderService.GetReturnList();
            }
            else
            {
                returns = orderService.GetReturnList().Where(p => p.Status == Status).OrderBy(p => p.CreatedAt).ToList();
            }
            return View(returns);
        }

        public ActionResult CompleteReturns(string Status = null)
        {
            List<PreOrderReturn> returns;

            if (Status == null)
            {
                returns = orderService.GetReturnList();
            }
            else
            {
                returns = orderService.GetReturnList().Where(p => p.Status == Status).OrderBy(p => p.CreatedAt).ToList();
            }
            return View(returns);
        }


        public ActionResult ReturnedItems(string Id, string OrderId)
        {
            PreOrder order = orderService.GetPreOrder(OrderId);

            orderService.UpdateReturnedStock(order);

            PreOrderReturn returnOrder = orderService.GetOrderReturn(Id);
            returnOrder.Status = "Return Complete";
            orderService.UpdateReturn(returnOrder);

            return RedirectToAction("DisplayReturns");
        }


        public ActionResult ManageReturn(string Id, string OrderId) //Id is the Return ID
        {
            ViewBag.StatusList = new List<string>() {
                "Pending",
                "Approved",
                "Rejected"
            };

            //Order order = orderService.GetOrder(Id);
            PreOrderReturn returnOrder = orderService.GetOrderReturn(Id);

            //returnOrder.OrderID = OrderId;

            return View(returnOrder);
        }

        [HttpPost]
        public ActionResult ManageReturn(PreOrderReturn returnedOrder, string Id)
        {
            PreOrderReturn returnOrder = orderService.GetOrderReturn(Id);

            returnOrder.Status = returnedOrder.Status;
            string retStatus = returnedOrder.Status;

            orderService.UpdateReturn(returnOrder);



            //Send Email to Customer
            string customer = returnOrder.Email; //Returns the customers email
            string fname = returnOrder.CustomerName; //Returns the customers first name

            string message = "";

            if (retStatus == "Approved")
            {
                message = "Hi " + fname + " Your order has been approved for return. Please drop off the order at the address below to receive your refund! <br/>" +
                "e-Library Address: Anton Lembede St, Durban Central, Durban, 4000 <br/>" +
                "Hours: 8am - 5pm (Monday-Sunday)";
            }
            else if (retStatus == "Rejected")
            {
                message = "Hi " + fname + " Unfortunately your order return request has been rejected. This is due to the 48 hour return period expiring! <br/>" +
                "We hope to see you soon!";

            }

            string receiver = customer;
            string subject = "E-Library Order Return";


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

            return RedirectToAction("DisplayReturns");
        }

    }
}