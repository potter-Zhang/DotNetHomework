using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement
{
    public class Customer
    {

        //public int CustomerId { get; set; }
        [Key]
        public int OrderId { get; set; }
        public string Name { get; set; }

        public Customer() {}
        public Customer(int id, string name)
        {
            OrderId = id;
            Name = name;
        }
    }
}
