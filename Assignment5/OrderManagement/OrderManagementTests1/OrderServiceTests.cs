using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        public void Init()
        {
            Good apple = new Good("apple", 10.0);
            Good banana = new Good("banana", 20.0);
            List<OrderDetail> details = new List<OrderDetail>();
            details.Add(new OrderDetail(apple, 5));
            details.Add(new OrderDetail(banana, 6));
            Customer customer = new Customer(1, "harry");
            Order order = new Order(1, customer, details);
            OrderService.AddOrder(order);
        }
        [TestMethod()]
        public void SearchTest()
        {
            Init();
            Assert.AreEqual("harry", OrderService.Search(Request.ID, "1").First().Customers.Name);
        }

        [TestMethod()]
        public void AddOrderTest()
        {
            Init();
            Good apple = new Good("apple", 10.0);
            Good banana = new Good("banana", 20.0);
            List<OrderDetail> details = new List<OrderDetail>();
            details.Add(new OrderDetail(apple, 5));
            details.Add(new OrderDetail(banana, 6));
            CollectionAssert.AreEqual(OrderService.Orders[0].OrderDetails, details);
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            Init();
            OrderService.DeleteOrder(1);
            Assert.IsTrue(OrderService.Orders.Count() == 0);
        }

        [TestMethod()]
        public void DeleteOrderDetailTest()
        {
            Init();
            OrderService.DeleteOrderDetail(1, "apple");
            Assert.IsTrue(OrderService.Orders[0].OrderDetails.Count() == 1);
        }

        [TestMethod()]
        public void AddOrderDetailTest()
        {
            Init();
            Good apple = new Good("apple", 10.0);
            Good banana = new Good("banana", 20.0);
            List<OrderDetail> details = new List<OrderDetail>();
            details.Add(new OrderDetail(apple, 5));
            details.Add(new OrderDetail(banana, 6));
            OrderService.AddOrderDetail(1, new OrderDetail(new Good("orange", 1), 100));
            details.Add(new OrderDetail(new Good("orange", 1), 100));
            Assert.AreEqual(details, OrderService.Orders[0].OrderDetails[0]);
        }

        

        [TestMethod()]
        public void DefaultSortingTest()
        {
            for (int i = 10; i > 0; i--) 
            {
                Order odr = new Order();
                
                odr.ID = i;
                OrderService.AddOrder(odr);
            }
            OrderService.DefaultSorting();
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list2.Add(i + 1);
                list1.Add(OrderService.Orders[i].ID);
            }
            CollectionAssert.AreEqual(list1, list2);
        }
    }
}