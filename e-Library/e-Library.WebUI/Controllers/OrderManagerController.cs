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
using System.Web;
using System.Web.Mvc;

namespace e_Library.WebUI.Controllers
{
    
    public class OrderManagerController : Controller
    {
        IOrderService orderService;
        IRepository<Driver> drivers;
        IRepository<OrderStatusModel> orderStatuses;

        public OrderManagerController(IOrderService OrderService, IRepository<Driver> driversContext, IRepository<OrderStatusModel> orderStatusContext) 
        {
            this.orderService = OrderService;
            drivers = driversContext;
            orderStatuses = orderStatusContext;
        }
        // GET: OrderManager
        [Authorize(Users = "21901959@dut4life.ac.za")]
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList();

            return View(orders);
        }

        public ActionResult DisplayOrdersWithFilter(string Status = null)
        {
            List<Order> orders;
            List<OrderStatusModel> statuses = orderStatuses.Collection().ToList();

            if (Status == null)
            {
                orders = orderService.GetOrderList().Where(p => p.OrderStatus != "Order Complete").ToList();
            }
            else
            {
                orders = orderService.GetOrderList().Where(p => p.OrderStatus == Status).ToList();
            }

            OrderStatusListViewModel model = new OrderStatusListViewModel();
            model.Orders = orders;
            model.OrderStatusModels = statuses;

            return View(model);
        }

        //Prepare Order
        public ActionResult PrepareOrder()
        {
            List<Order> orders = orderService.GetOrderList();
            orders = orderService.GetOrderList().Where(p => (p.Driver == "" && p.DeliveryMethod == "Standard Delivery" || p.DeliveryMethod == "Express Delivery")).ToList();

            return View(orders);
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

            Order order = orderService.GetOrder(Id);
            order.Drivers = drivers.Collection();

            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrder(Order updatedOrder, string Id)
        {
            Order order = orderService.GetOrder(Id);

            order.OrderStatus = updatedOrder.OrderStatus;
            order.Driver = updatedOrder.Driver;
            orderService.UpdateOrder(order);

            return RedirectToAction("DisplayOrdersWithFilter");
        }


        //Driver Portal
        public ActionResult DriverPortal(string Id)
        {
            ViewBag.StatusList = new List<string>() {
                "Out For Delivery",
                "Order Complete"
            };

            Order order = orderService.GetOrder(Id);
            order.Drivers = drivers.Collection();

            return View(order);
        }
        [HttpPost]
        public ActionResult DriverPortal(Order updatedOrder, string Id)
        {
            //Updating Order Status
            Order order = orderService.GetOrder(Id);

            order.OrderStatus = updatedOrder.OrderStatus;

            orderService.UpdateOrder(order);


            return RedirectToAction("Index");
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


    }
}