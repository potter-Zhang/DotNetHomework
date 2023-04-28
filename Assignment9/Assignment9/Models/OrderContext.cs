using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.EntityFramework;

using Microsoft.Extensions.Options;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Assignment9.Models
{

    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> Details { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Good> Goods { get; set; }



    }
}
