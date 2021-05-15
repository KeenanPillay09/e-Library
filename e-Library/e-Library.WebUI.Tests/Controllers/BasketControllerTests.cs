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

            var httpContext = new MockHttpContext();


            IBasketService basketService = new BasketService(books, baskets);
            var controller = new BasketController(basketService);
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

            books.Insert(new Book() { Id = "1", Price = 10.00m });
            books.Insert(new Book() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { BookId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { BookId = "2", Quantity = 1 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(books, baskets);


            var controller = new BasketController(basketService);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);


            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(25.00m, basketSummary.BasketTotal);


        }
    }
}
