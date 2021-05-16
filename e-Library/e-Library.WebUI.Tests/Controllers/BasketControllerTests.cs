using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using e_Library.Core.Models;
using e_Library.Core.Contracts;
using e_Library.WebUI.Tests.Mocks;
using e_Library.WebUI.Controllers;
using System.Linq;
using e_Library.Services;
using System.Web.Mvc;
using e_Library.Core.ViewModels;
using System.Security.Principal;

namespace e_Library.WebUI.Tests.Controllers
{
    /// <summary>
    /// Summary description for BasketControllerTests
    /// </summary>
    [TestClass]
    public class BasketControllerTests
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //setup
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Book> books = new MockContext<Book>();
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>();

            var httpContext = new MockHttpContext();


            IBasketService basketService = new BasketService(books, baskets);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService,orderService,customers);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            //basketService.AddToBasket(httpContext, "1");
            controller.AddToBasket("1");

            Basket basket = baskets.Collection().FirstOrDefault();


            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().BookId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Book> books = new MockContext<Book>();
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>();

            books.Insert(new Book() { Id = "1", Price = 10.00m });
            books.Insert(new Book() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { BookId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { BookId = "2", Quantity = 1 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(books, baskets);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService, orderService,customers);


            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);


            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(25.00m, basketSummary.BasketTotal);
        }

        [TestMethod]
        public void CanCheckoutAndCreateOrder()
        {
            IRepository<Customer> customers = new MockContext<Customer>();
            IRepository<Book> books = new MockContext<Book>();
            books.Insert(new Book() { Id = "1", Price = 10.00m });
            books.Insert(new Book() { Id = "2", Price = 5.00m });

            IRepository<Basket> baskets = new MockContext<Basket>();
            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { BookId = "1", Quantity = 2, BasketId = basket.Id });
            basket.BasketItems.Add(new BasketItem() { BookId = "1", Quantity = 1, BasketId = basket.Id });

            baskets.Insert(basket);

            IBasketService basketService = new BasketService(books, baskets);
            IRepository<Order> orders = new MockContext<Order>();
            IOrderService orderService = new OrderService(orders);

            customers.Insert(new Customer() { Id = "1", Email = "keenanpillay1234@gmail.com",City="Durban",FirstName="Keenan",LastName="Pillay",Street="12 St Thomas Rd" , ZipCode = "90210" });

            IPrincipal FakeUser = new GenericPrincipal(new GenericIdentity("keenanpillay1234@gmail.com", "Forms"), null);
            var controller = new BasketController(basketService, orderService,customers);
            var httpContext = new MockHttpContext();
            httpContext.User = FakeUser;
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket")
            {
                Value = basket.Id
            });

            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            Order order = new Order();
            controller.Checkout(order);

            //assert
            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.BasketItems.Count);

            Order orderInRep = orders.Find(order.Id);
            Assert.AreEqual(2, orderInRep.OrderItems.Count);

        }
    }
}
