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
        [TestMethod()]
        public void SearchTest()
        {
            Order odr = new Order();
            odr.Customer = "harry";
            odr.ID = 1;
            OrderDetail detail = new OrderDetail("box", 1);

            odr.OrderDetails.Add(detail);
            OrderService.AddOrder(odr);

            Assert.AreEqual(odr.Customer, OrderService.Search(Request.ID, "1").First().Customer);
        }

        [TestMethod()]
        public void AddOrderTest()
        {
            Order odr = new Order();
            odr.Customer = "harry";
            odr.ID = 1;
            List<OrderDetail> odrlist;
            odrlist = new List<OrderDetail>();
            OrderDetail detail = new OrderDetail("box", 1);
            odrlist.Add(detail);
            odr.OrderDetails.Add(detail);
            OrderService.AddOrder(odr);
            Assert.AreEqual(odr.Customer, OrderService.Orders[0].Customer);
            Assert.AreEqual(odr.ID, OrderService.Orders[0].ID);
            CollectionAssert.AreEqual(OrderService.Orders[0].OrderDetails, odrlist);
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            Order odr = new Order();
            odr.Customer = "harry";
            odr.ID = 1;    
            OrderDetail detail = new OrderDetail("box", 1);
            
            odr.OrderDetails.Add(detail);
            OrderService.AddOrder(odr);
            OrderService.DeleteOrder(1);
            Assert.IsTrue(OrderService.Orders.Count() == 0);
        }

        [TestMethod()]
        public void DeleteOrderDetailTest()
        {
            Order odr = new Order();
            odr.Customer = "harry";
            odr.ID = 1;
            OrderDetail detail = new OrderDetail("box", 1);

            odr.OrderDetails.Add(detail);
            OrderService.AddOrder(odr);
            OrderService.DeleteOrderDetail(1, "box");
            Assert.IsTrue(OrderService.Orders[0].OrderDetails.Count() == 0);
        }

        [TestMethod()]
        public void AddOrderDetailTest()
        {
            Order odr = new Order();
            odr.Customer = "harry";
            odr.ID = 1;
            OrderService.AddOrder(odr);
            OrderDetail detail;
            OrderService.AddOrderDetail(1, detail = new OrderDetail("box", 1));
            Assert.AreEqual(detail, OrderService.Orders[0].OrderDetails[0]);
        }

        

        [TestMethod()]
        public void DefaultSortingTest()
        {
            for (int i = 10; i > 0; i--) 
            {
                Order odr = new Order();
                odr.Customer = "harry";
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