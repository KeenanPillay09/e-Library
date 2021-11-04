using e_Library.Core.Contracts;
using e_Library.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Library.WebUI.Controllers
{
    public class ChartController : Controller
    {
        IRepository<Book> books;

        public ChartController(IRepository<Book> booksContext)
        {
            books = booksContext;
        }


        public ActionResult ChartOptions()
        {
            return View();
        }

        // GET: Chart
        public ActionResult Index()
        {
            List<Book> book = books.Collection().ToList();
            return View(book);
        }

        //MOST POPULAR BOOK
        public ActionResult MostPopularChart()
        {
            List<Book> book = books.Collection().OrderByDescending(p => p.NumOrders).ToList();
            return View(book);
        }
        public ActionResult ShowMostPopular()
        {
            return View();
        }

        //LOW STOCK
        public ActionResult LowStockChart()
        {
            List<Book> book = books.Collection().OrderBy(p => p.Stock).ToList();
            return View(book);
        }
        public ActionResult ShowLowStock()
        {
            return View();
        }

        //SHOW MOST NUMBER SALES
        public ActionResult NumberSalesChart()
        {
            List<Book> book = books.Collection().OrderByDescending(p => p.NumSales).ToList();
            return View(book);
        }
        public ActionResult ShowNumberSales()
        {
            return View();
        }

        //SHOW TOTAL SALES
        public ActionResult TotalSalesChart()
        {
            List<Book> book = books.Collection().OrderByDescending(p => p.TotalSales).ToList();
            return View(book);
        }
        public ActionResult ShowTotalSales()
        {
            return View();
        }



    }
}