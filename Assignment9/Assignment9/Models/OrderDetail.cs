using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment9.Models
{
    public class OrderDetail
    {

        public int OrderId { get; set; }
        [Key]
        public int DetailId { get; set; }
        public int Number { get; set; }

        public double ProductPrice { get; set; }

        public OrderDetail() { }
        public OrderDetail(int orderId, int detailId, int number, double productPrice)
        {
            OrderId = orderId;
            DetailId = detailId;
            Number = number;
            ProductPrice = productPrice;
        }
    }
}
