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
        public Customer Customers { get; set; }
        public string CustomerName 
        { 
            get
            {
                return Customers.Name;
            }
        }

        public string Date
        {
            get; set;
        }
        //public int Price { get; set; }

        public Order(int iD, Customer customer, List<OrderDetail> details)
        {
            ID = iD;
            Customers = customer;
            OrderDetails = details;
        }

        public double Sum
        {
            get
            {
                double sum = 0;
                OrderDetails.ForEach(x => sum += x.TotalPrice);
                return sum;
            }
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
            s.Append($"order id: {ID}\ncustomer: {Customers.Id}\t{Customers.Name}\nproduct name\t price\tnumber\ttotal\n");
            
            foreach (OrderDetail orderdetail in OrderDetails)
            {
                s.AppendLine(orderdetail.ToString());
            }
            s.AppendLine($"total = {Sum}");
            return s.ToString();
        }
    }
}
