using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderManagement
{
    public enum Request { ID, ProductName, Customer, All };
    public class SearchOrderResults
    {
        public int OrderSeq { get; set; }
        public string Day { get; set; }
        public string CustomerName { get; set; }
        public double Sum { get; set; }
    }

    public class SearchOrderDetailResults
    {
        public string GoodsName { get; set; }
        public double GoodPrice { get; set; }
        public int Number { get; set; }
        public double TotalPrice { get; set; }
    }

    public class OrderService
    {
        //private static List<Order> orders = new List<Order>();
        //private static List<OrderDetail> details;
        
        
       
        public OrderService()
        {
           
        }

        public static List<SearchOrderDetailResults> SearchOrderDetail(int orderId)
        {
            using (OrderContext ctx = new OrderContext())
            {
                var query = from detail in ctx.Details
                            join good in ctx.Goods on detail.DetailId equals good.DetailId
                            where detail.OrderId == orderId
                            select new SearchOrderDetailResults
                            {
                                GoodsName = good.Name,
                                GoodPrice = good.Price,
                                Number = detail.Number,
                                TotalPrice = detail.ProductPrice,
                             

                            };
                return query.ToList();
            }
        }
    public static int Seq2Id(int seq)
    {
            using (OrderContext ctx = new OrderContext())
            {
                var query = from order in ctx.Orders
                            where order.SeqNum == seq
                            select order.OrderId;
                return query.First();
            }
        
    }
    public static List<SearchOrderResults> SearchOrder(Request req, string parameter)
    {
        //IEnumerable<Order> query;
        using (OrderContext ctx = new OrderContext())
        {
            switch (req)
            {
                case Request.ID:
                    int id = int.Parse(parameter);
                    var query1 = from odr in ctx.Orders
                                join ctm in ctx.Customers on odr.OrderId equals ctm.OrderId
                                where odr.OrderId == id
                                select new SearchOrderResults
                                {
                                    OrderSeq = odr.SeqNum,
                                    Day = odr.OrderTime,
                                    CustomerName = ctm.Name,
                                    Sum = odr.Sum
                                };

                    return query1.ToList();
                    
                case Request.Customer:
                        var query2 = from odr in ctx.Orders
                                    join ctm in ctx.Customers on odr.OrderId equals ctm.OrderId
                                    where ctm.Name == parameter
                                    select new SearchOrderResults
                                    {
                                        OrderSeq = odr.SeqNum,
                                        Day = odr.OrderTime,
                                        CustomerName = ctm.Name,
                                        Sum = odr.Sum
                                    };
                        return query2.ToList();
                case Request.ProductName:
                    var query3 = from odr in ctx.Orders
                                    join ctm in ctx.Customers on odr.OrderId equals ctm.OrderId
                                    join det in ctx.Details on odr.OrderId equals det.OrderId
                                    join good in ctx.Goods on det.DetailId equals good.DetailId
                                    where good.Name == parameter
                                    select new SearchOrderResults 
                                    { 
                                        OrderSeq = odr.SeqNum, 
                                        Day = odr.OrderTime, 
                                        CustomerName = ctm.Name,
                                        Sum = odr.Sum
                                        
                                    };
                    return query3.ToList();
                case Request.All:
                     var query4 = from odr in ctx.Orders
                           join ctm in ctx.Customers on odr.OrderId equals ctm.OrderId
                           select new SearchOrderResults
                           {
                               OrderSeq = odr.SeqNum,
                               Day = odr.OrderTime,
                               CustomerName = ctm.Name,
                               Sum = odr.Sum
                           };
                        return query4.ToList();
                    default: throw new Exception("error: Request not found!");

            }
        }
        

    }


        public static void AddOrder(Order newOrder)
        {
            //CheckOrderId(newOrder.OrderId);
            using (OrderContext ctx = new OrderContext())
            {
                ctx.Orders.Add(newOrder);
                ctx.SaveChanges();
            }
            

        }

        public static void AddCustomer(Customer customer)
        {
            using (OrderContext ctx = new OrderContext())
            {
                ctx.Customers.Add(customer);
                ctx.SaveChanges();
            }
        }
        public static void UpdateCustomer(Customer customer)
        {
            
            using (var ctx = new OrderContext())
            {
                Customer ctm = ctx.Customers.Find(customer.OrderId);
                ctx.Entry(ctm).State = EntityState.Modified;
                //odr.SequenceNum = order.SequenceNum;
                ctm.Name = customer.Name;
                ctx.SaveChanges();
            }
        }

        public static void CheckSeq(int seq, int oldseq)
        {
            if (seq == oldseq)
                return;
            using (OrderContext ctx = new OrderContext())
            {
                var q = ctx.Orders.FirstOrDefault(o => o.SeqNum == seq);
                if (q != null)
                    throw new Exception("orderId already exists!");
            }
        }
        public static void CheckOrderId(int id)
        {
            using (OrderContext ctx = new OrderContext())
            {
                var q = ctx.Orders.FirstOrDefault(o => o.OrderId == id);
                if (q != null)
                    throw new Exception("orderId already exists!");
            }
        }

        public static void SaveOrder(int orderId)
        {
            using (OrderContext ctx = new OrderContext())
            {
                var q = ctx.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (q != null)
                {
                    double sum = 0;
                    var s = from detail in ctx.Details
                            where detail.OrderId == orderId
                            select detail.ProductPrice;
                    foreach (double price in s)
                    {
                        sum += price;
                    }
                    q.Sum = sum;
                    ctx.SaveChanges();
                }
            }
        }

        public static void DeleteOrder(int id)
        {
            
            using (OrderContext ctx = new OrderContext())
            {
                var q1 = ctx.Orders.FirstOrDefault(o => o.SeqNum == id);
                if (q1 != null)
                {
                    
                    var q2 = from detail in ctx.Details
                             where detail.OrderId == q1.OrderId
                             select detail;
                    var q3 = from ctm in ctx.Customers
                             where ctm.OrderId == q1.OrderId
                             select ctm;
                    foreach(var d in q2)
                    {
                        ctx.Details.Remove(d);
                    }
                    foreach (var c in q3)
                    {
                        ctx.Customers.Remove(c);
                    }
                    ctx.Orders.Remove(q1);
                    //ctx.Orders.RemoveRange(q2));
                    ctx.SaveChanges();

                }
                
            }
            //orders.Remove(query.First());
        }

   

        public static void DeleteOrderDetail(OrderDetail orderdetail)
        {
            using (var ctx = new OrderContext())
            {
                var d = ctx.Details.FirstOrDefault(o => o.DetailId == orderdetail.DetailId);
                var g = ctx.Goods.FirstOrDefault(o => o.DetailId == orderdetail.DetailId);
                if (d != null)
                {
                    ctx.Details.Remove(d);
                    ctx.Goods.Remove(g);
                    ctx.SaveChanges();
                }
                
            }

        }

        public static OrderDetail FindOrderDetail(int orderId, string name)
        {
            using (var ctx = new OrderContext())
            {
                var q = from detail in ctx.Details 
                        join good in ctx.Goods on detail.DetailId equals good.DetailId
                        where detail.OrderId == orderId && good.Name == name
                        select detail;
                return q.ToList()[0];
            }
        }

        public static void CheckGood(Good good)
        {
            using (var ctx = new OrderContext())
            {
                var q = from g in ctx.Goods
                        where g.Name == good.Name && g.OrderId == good.OrderId
                        select g;
                if (q.Count() > 0)
                    throw new Exception("goods' name should differ");
                
            }
        }

        public static int OrderCount()
        {
            using (var ctx = new OrderContext())
            {
                try
                {
                    int q = ctx.Orders.Max(o => o.OrderId);

                    return q;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static void UpdateOrder(Order order, int seq)
        {
            //CheckOrderId(order.OrderId);
            using (var ctx = new OrderContext())
            {
                Order odr = ctx.Orders.Find(Seq2Id(order.SeqNum));
                
                ctx.Entry(odr).State= EntityState.Modified;
                //odr.SequenceNum = order.SequenceNum;
                odr.Sum = order.Sum;
                odr.OrderTime = order.OrderTime;
                odr.SeqNum = seq;
                
                ctx.SaveChanges();
            }
        }

        public static void UpdateOrderDetail(OrderDetail orderdetail, int num, int p)
        {
            using (var ctx = new OrderContext())
            {
                ctx.Entry(orderdetail).State = EntityState.Modified;
                orderdetail.Number = num;
                orderdetail.ProductPrice = p;
                ctx.SaveChanges();
            }
        }

        public static void AddOrderDetail(OrderDetail orderdetail)
        {
            using (var ctx = new OrderContext())
            {
                ctx.Details.Add(orderdetail);
                ctx.SaveChanges();
            }
        }

        public static void AddGood(Good good)
        {
            using (var ctx = new OrderContext())
            {
                ctx.Goods.Add(good);
                ctx.SaveChanges();
            }
        }

        public static void SortOrders(Comparison<Order> cmp)
        {
            //orders.Sort(cmp);
        }

        public static void DefaultSorting()
        {
            //orders.Sort((x, y) => x.ID.CompareTo(y.ID));
        }


    }
}
