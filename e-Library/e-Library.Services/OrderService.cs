using e_Library.Core.Contracts;
using e_Library.Core.Models;
using e_Library.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> orderContext;
        IRepository<Return> returnContext;
        IRepository<Book> bookContext;

        public OrderService(IRepository<Order> OrderContext, IRepository<Return> ReturnContext, IRepository<Book> BookContext)
        {
            this.orderContext = OrderContext;
            this.returnContext = ReturnContext;
            this.bookContext = BookContext;
        }

        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    BookId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    BookName = item.BookName,
                    Quantity = item.Quantity
                });
            }

            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }

        public List<Order> GetOrderList() //Returns list of orders
        {
            return orderContext.Collection().ToList();
        }

        public Order GetOrder(string Id) //Returns an individual order
        {
            return orderContext.Find(Id);
        }

        public void UpdateOrder(Order updatedOrder) //Updates order (Order Status)
        {
            orderContext.Update(updatedOrder);
            orderContext.Commit();
        }

        public List<Return> GetReturnList() //Returns list of returns
        {
            return returnContext.Collection().ToList();
        }

        public Return GetOrderReturn(string Id) //Returns an individual return
        {
            return returnContext.Find(Id);
        }

        public void CreateReturn(Return returnedOrder)
        {
            returnContext.Insert(returnedOrder);
            returnContext.Commit();
        }

        public void UpdateReturn(Return updatedReturn)
        {
            returnContext.Update(updatedReturn);
            returnContext.Commit();
        }

        public void UpdateReturnedStock(Order ordertoReturn)
        {
            int iNumItems = ordertoReturn.OrderItems.Count;

            List<OrderItem> orderItems = ordertoReturn.OrderItems.ToList();
            string bId = "";
            string bName = "";
            int bQuantity = 0;
            for (int i = 0; i < iNumItems; i++)
            {
                bId = orderItems[i].BookId.ToString();
                bQuantity = orderItems[i].Quantity;

                bName = orderItems[i].BookName.ToString();

                Book book = new Book();
                var findBookId = bookContext.Collection().Where(d => d.Name == bName).FirstOrDefault();
                string newId = findBookId.Id;


                Book bookToEdit = bookContext.Find(newId);
                bookToEdit.Stock -= bQuantity;
                bookContext.Commit();
            }
        }
    }
}
