using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using e_Library.WebUI;
using e_Library.WebUI.Controllers;
using e_Library.Core.Contracts;
using e_Library.Core.Models;
using e_Library.Core.ViewModels;

namespace e_Library.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        //[TestMethod]
        //public void Index()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void About()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.About() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        //}

        //[TestMethod]
        //public void Contact()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Contact() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

            [TestMethod]
            public void IndexPageDoesReturnProducts()
            {
                IRepository<Book> bookContext = new Mocks.MockContext<Book>();
                IRepository<BookGenre> bookGenreContext = new Mocks.MockContext<BookGenre>();
                IRepository<BookAuthor> bookAuthorContext = new Mocks.MockContext<BookAuthor>();

                bookContext.Insert(new Book());

                PurchaseBookController controller = new PurchaseBookController(bookContext, bookGenreContext, bookAuthorContext);

                var result = controller.Index() as ViewResult;
                var viewModel = (BookListViewModel)result.ViewData.Model;

                Assert.AreEqual(1, viewModel.Books.Count());

            }
        
    }
}
