using e_Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Library.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection") //Looks in Web.Config/App.Config and looks for Default Connection string
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<PreOrderBook> PreOrderBooks { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PreOrder> PreOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PreOrderItem> PreOrderItems { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<OrderStatusModel> OrderStatusModels { get; set; }
        public DbSet<PreOrderStatusModel> PreOrderStatusModels { get; set; }
        public DbSet<Return> Returns { get; set; }

    }
}
