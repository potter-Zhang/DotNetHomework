using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{
    public enum Request { ID, ProductName, Customer, All };
    public class OrderService
    {
        private static List<Order> orders = new List<Order>();
        //private static List<OrderDetail> details;
        public static List<Order> Orders
        {
            get
            {
                return orders;
            }
        }
    

    public static void ShowOrders(IEnumerable<Order> query)
    {

        foreach (var order in query)
        {
            Console.Write(order.ToString());
        }
        Console.WriteLine("-----------");
    }

    public static IEnumerable<Order> Search(Request req, string parameter)
    {
        IEnumerable<Order> query;
        switch (req)
        {
            case Request.ID:
                int id = int.Parse(parameter);
                query = from order in orders
                        where order.ID == id
                        orderby order.ID
                        select order;
                return query;
            case Request.Customer:
                query = from order in orders
                        where order.Customers.Name == parameter
                        orderby order.ID
                        select order;
                return query;
            case Request.ProductName:
                query = from order in orders
                        where order.OrderDetails.Any(x => x.Goods.Name == parameter)
                        orderby order.ID
                        select order;
                return query;
            case Request.All:
                query = from order in orders
                        orderby order.ID
                        select order;
                return query;
            default: throw new Exception("error: Request not found!");

        }

    }

    public static void AddOrder(Order newOrder)
    {
        var query = Search(Request.ID, newOrder.ID.ToString());
        if (query.Count() > 0)
            throw new Exception("error: invalid order!");
        orders.Add(newOrder);
    }

    public static void DeleteOrder(int id)
    {
        var query = Search(Request.ID, id.ToString());
        if (query.Count() == 0)
            throw new Exception("error: can't delete, order missing!");
        orders.Remove(query.First());
    }

    public static void DeleteOrderDetail(int id, string name)
    {
        var query = Search(Request.ID, id.ToString());
        if (query.Count() == 0)
            throw new Exception("error: can't delete, order missing!");
        var q = from detail in query.First().OrderDetails
                where detail.Goods.Name == name
                select detail;
        if (q.Count() == 0)
            throw new Exception("error: can't delete, orderdetail missing!");
        query.First().OrderDetails.Remove(q.First());

    }

    public static void AddOrderDetail(int id, OrderDetail orderdetail)
    {
        var q = Search(Request.ID, id.ToString());
        if (q.Count() == 0)
            throw new Exception("error: order not found!");
        Order odr = q.First();
        var query = from d in odr.OrderDetails
                    where d.Equals(orderdetail)
                    select d;
        if (query.Count() > 0)
            throw new Exception("error: invalid order!");
        odr.OrderDetails.Add(orderdetail);
    }

    public static void SortOrders(Comparison<Order> cmp)
    {
        orders.Sort(cmp);
    }

    public static void DefaultSorting()
    {
        orders.Sort((x, y) => x.ID - y.ID);
    }


    }
}
