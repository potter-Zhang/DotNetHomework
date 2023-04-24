using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.EntityFramework;
using System.Data.Entity;

namespace OrderManagement
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class OrderContext : DbContext
    {
        public OrderContext() : base("OrderDataBase") 
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OrderContext>());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> Details { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Good> Goods { get; set; }



    }
}
