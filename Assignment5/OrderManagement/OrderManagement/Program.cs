namespace OrderManagement
{
    class Test
    {
        static void Main()
        {
            Console.WriteLine("Order Management");
            string op1;
            string op2;
            string op3;
            bool exit = false;
            try
            {
                while (!exit)
                {
                    ShowHelpInfo();
                    op1 = Console.ReadLine();
                    switch (int.Parse(op1))
                    {
                        case 1:
                            Console.WriteLine("1. Search by id");
                            Console.WriteLine("2. Search by costomer");
                            Console.WriteLine("3. Search by product name");
                            Console.WriteLine("4. List all of the orders");
                            op2 = Console.ReadLine();
                            int id, num;
                            switch (int.Parse(op2))
                            {
                                case 1:
                                    Console.Write("id = ");
                                    op3 = Console.ReadLine();
                                    OrderService.ShowOrders(OrderService.Search(Request.ID, op3));
                                    break;
                                case 2:
                                    Console.Write("costomer = ");
                                    op3 = Console.ReadLine();
                                    OrderService.ShowOrders(OrderService.Search(Request.Customer, op3));
                                    break;
                                case 3:
                                    Console.Write("product name = ");
                                    op3 = Console.ReadLine();
                                    OrderService.ShowOrders(OrderService.Search(Request.ProductName, op3));
                                    break;
                                case 4:
                                    OrderService.ShowOrders(OrderService.Search(Request.All, ""));
                                    break;
                                default:
                                    Console.WriteLine("invalid option");
                                    break;

                            }

                            break;
                        case 2:
                            var odr = new Order();
                            Console.Write("order id: ");
                            odr.ID = int.Parse(Console.ReadLine());
                            Console.Write("Customer: ");
                            odr.Customer = Console.ReadLine();
                           

                            OrderService.AddOrder(odr);
                            break;
                        case 3:
                            Console.Write("order id: ");
                            id = int.Parse(Console.ReadLine());
                            OrderService.DeleteOrder(id);
                            break;
                        case 4:
                            Console.Write("order id: ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("product name: ");

                            string name = Console.ReadLine();
                            Console.Write("number of products: ");
                            num = int.Parse(Console.ReadLine());
                            OrderDetail orderdetail = new OrderDetail(name, num);
                            OrderService.AddOrderDetail(id, orderdetail);
                            break;
                        case 5:
                            Console.Write("order id: ");
                            id = int.Parse(Console.ReadLine());
                            Console.Write("name of order detail: ");
                            name = Console.ReadLine();
                            OrderService.DeleteOrderDetail(id, name);
                            break;
                        case 6:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("invalid operation!");
                            break;
                    }

                }
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message); 
            }
        }

        static void ShowHelpInfo()
        {
            Console.WriteLine("1. query for orders");
            Console.WriteLine("2. add orders");
            Console.WriteLine("3. delete orders");
            Console.WriteLine("4. add order details");
            Console.WriteLine("5. delete order details");
            Console.WriteLine("6. exit");
        }
    }
}