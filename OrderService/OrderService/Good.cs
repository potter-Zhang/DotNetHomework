using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{
    public class Good
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Good(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
