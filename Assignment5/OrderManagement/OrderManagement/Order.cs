using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderManagement.OrderService;

namespace OrderManagement
{
    public class Order
    {
        
        public int ID { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public string Customer { get; set; }
        //public int Price { get; set; }

        public Order(int id, List<OrderDetail> orderdetails, string customer)
        {
            ID = id;
            OrderDetails = orderdetails;
            Customer = customer;
            //Price = price;
        }
        public Order()
        {
            OrderDetails = new List<OrderDetail>();   
        }


        public override bool Equals(object obj)
        {
            var other = obj as Order;
            if (other == null) 
                return false;
            return ID == other.ID;
        }

        public override string ToString()
        {
            //return $"order id: {ID}\n";
            StringBuilder s = new StringBuilder();
            s.Append("--------------\n");
            s.Append($"order id: {ID}\ncustomer: {Customer}\nproduct name\t number\n");
            
            foreach (OrderDetail orderdetail in OrderDetails)
            {
                s.AppendLine(orderdetail.ToString());
            }
            
            return s.ToString();
        }
    }
}
