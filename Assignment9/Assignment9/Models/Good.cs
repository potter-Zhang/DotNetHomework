using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment9.Models
{
    public class Good
    {
        [Key]
        public int DetailId { get; set; }
        public int OrderId { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }

        public Good() { }
        public Good(int did, int orderId, string name, double price)
        {
            DetailId = did;
            OrderId = orderId;
            Name = name;
            Price = price;
        }

    }
}
