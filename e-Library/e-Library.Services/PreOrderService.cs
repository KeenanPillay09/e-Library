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
    public class PreOrderService : IPreOrderService
    {
        IRepository<PreOrder> orderContext;
        IRepository<PreOrderReturn> returnContext;
        IRepository<PreOrderBook> bookContext;
        public PreOrderService(IRepository<PreOrder> OrderContext, IRepository<PreOrderReturn> ReturnContext, IRepository<PreOrderBook> BookContext)
        {
            this.orderContext = OrderContext;
            this.returnContext = ReturnContext;
            this.bookContext = BookContext;
        }

        public void CreatePreOrder(PreOrder baseOrder)
        {
            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }

        public List<PreOrder> GetPreOrderList() //Returns list of orders
        {
            return orderContext.Collection().ToList();
        }

        public PreOrder GetPreOrder(string Id) //Returns an individual order
        {
            return orderContext.Find(Id);
        }

        public void UpdatePreOrder(PreOrder updatedOrder) //Updates order (Order Status)
        {
            orderContext.Update(updatedOrder);
            orderContext.Commit();
        }

        public List<PreOrderReturn> GetReturnList() //Returns list of returns
        {
            return returnContext.Collection().ToList();
        }

        public PreOrderReturn GetOrderReturn(string Id) //Returns an individual return
        {
            return returnContext.Find(Id);
        }

        public void CreateReturn(PreOrderReturn returnedOrder)
        {
            returnContext.Insert(returnedOrder);
            returnContext.Commit();
        }

        public void UpdateReturn(PreOrderReturn updatedReturn)
        {
            returnContext.Update(updatedReturn);
            returnContext.Commit();
        }

        public void UpdateReturnedStock(PreOrder ordertoReturn)
        {

            string bName = "";
            bName = ordertoReturn.BookName;

            PreOrderBook book = new PreOrderBook();
            var findBookId = bookContext.Collection().Where(d => d.Name == bName).FirstOrDefault();
            string newId = findBookId.Id;


            PreOrderBook bookToEdit = bookContext.Find(newId);
            bookToEdit.Stock -= 1;
            bookContext.Commit();
            
        }
    }
}
