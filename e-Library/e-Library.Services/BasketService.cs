using e_Library.Core.Contracts;
using e_Library.Core.Models;
using e_Library.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace e_Library.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Book> bookContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Book> BookContext, IRepository<Basket> BasketContext)
        {
            this.basketContext = BasketContext;
            this.bookContext = BookContext;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;

                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }

            if (basket==null)
            {
                basket = CreateNewBasket(httpContext);
            }

            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string bookId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.BookId == bookId);

            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    BookId = bookId,
                    Quantity = 1
                };

                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }
            basketContext.Commit();

            //Increase Number of Sales

            Book bookToEdit = bookContext.Find(bookId);

            int bookQuantity = item.Quantity;
            decimal bookPrice = bookToEdit.Price;

            bookToEdit.NumSales += 1;
            bookToEdit.TotalSales += bookPrice;

            //Decrease Stock

            bookToEdit.Stock -= 1;

            //Add to Number of Orders



            if (bookQuantity < 2)
            {
                bookToEdit.NumOrders += 1;
            }

            bookContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
            //Decrease Number of Sales

            string bookId = item.BookId;
            Book bookToEdit = bookContext.Find(bookId);

            int bookQuantity = item.Quantity;
            decimal bookPrice = bookToEdit.Price;

            bookToEdit.NumSales -= bookQuantity;
            bookToEdit.TotalSales -= (bookPrice * bookQuantity);

            //Increase Stock

            bookToEdit.Stock += bookQuantity;

            //Decrease Number of Orders
            bookToEdit.NumOrders -= 1;

            bookContext.Commit();
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);

            if (basket != null)
            {
                var results = (from b in basket.BasketItems
                               join p in bookContext.Collection() on b.BookId equals p.Id
                               select new BasketItemViewModel()
                               {
                                   Id = b.Id,
                                   Quantity = b.Quantity,
                                   BookName = p.Name,
                                   Image = p.Image,
                                   Price = p.Price
                               }
                              ).ToList();

                return results;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);
            if (basket != null)
            {
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();

                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in bookContext.Collection() on item.BookId equals p.Id
                                        select item.Quantity * p.Price).Sum();

                model.BasketCount = basketCount ?? 0; //Assigns values to model. If its null (??) then it assigns the value to null/0.0
                model.BasketTotal = basketTotal ?? decimal.Zero;

                return model;
            }
            else
            {
                return model;
            }
        }

        public decimal BasketTotal(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            decimal bTotal = 0;

            if (basket != null)
            {
                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in bookContext.Collection() on item.BookId equals p.Id
                                        select item.Quantity * p.Price).Sum();

                bTotal = basketTotal ?? decimal.Zero;
            }

            return bTotal;
        }

        public void ClearBasket(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            basket.BasketItems.Clear();
            basketContext.Commit();
        }
    }
}
