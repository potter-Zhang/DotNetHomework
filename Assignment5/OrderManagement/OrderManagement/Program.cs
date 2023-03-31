namespace OrderManagement
{
    class Test
    {
        static void Main()
        {
            Good apple = new Good("apple", 10.0);
            Good banana = new Good("banana", 20.0);
            List<OrderDetail> details = new List<OrderDetail>();
            details.Add(new OrderDetail(apple, 5));
            details.Add(new OrderDetail(banana, 6));
            Customer customer = new Customer(1, "harry");
            Order order = new Order(1, customer, details);
            OrderService.AddOrder(order);
            OrderService.ShowOrders(OrderService.Search(Request.ID, "1"));

        }
    }
}