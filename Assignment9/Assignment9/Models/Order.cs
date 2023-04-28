using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment9.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        //public int SequenceNum { get; set; }
        public string OrderTime { get; set; }
        public int SeqNum { get; set; }
        public double Sum { get; set; }

        public Order() { }
        public Order(int orderId, string date, double sum, int seqNum)
        {
            OrderId = orderId;
            //SequenceNum = seq;
            OrderTime = date;
            Sum = sum;
            SeqNum = seqNum;
        }
    }




}
