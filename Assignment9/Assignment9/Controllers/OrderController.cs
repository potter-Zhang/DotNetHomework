using Assignment9.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using static Assignment9.Controllers.OrderController;

namespace Assignment9.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        public class DetailInfo
        {
            public int num { get; set; }
            public Good good { get; set; }
        }
        public class OrderInfo
        {
            public int seqNum { get; set; }
            public string customerName { get; set; }
            public List<DetailInfo> detailInfos { get; set; }

        }
        public class Helper
        {
            public static void _AddOrder(OrderInfo orderInfo)
            {
                int orderId = OrderService.OrderCount() + 1;
                
                OrderService.AddCustomer(new Customer(orderId, orderInfo.customerName));
                double sum = 0;
                for (int i = 0; i < orderInfo.detailInfos.Count; i++)
                {
                    Good good = orderInfo.detailInfos[i].good;
                    int num = orderInfo.detailInfos[i].num;
                    good.OrderId = orderId;
                    good.DetailId = OrderService.OrderDetailCount() + 1;
                    OrderService.CheckGood(good);
                    OrderService.AddGood(good);
                    OrderService.AddOrderDetail(new OrderDetail(orderId, good.DetailId, num, good.Price * num));
                    sum += good.Price * num;
                }

                OrderService.AddOrder(new Order(orderId, DateTime.Now.ToString("yyyy-MM-dd"), sum, orderInfo.seqNum));
            }

        }


        OrderService odrs;
        int orderId;
        public OrderController(OrderService o)
        {
            odrs = o;
        }

        [HttpGet(Name = "GetAllOrders")]
        public IEnumerable<SearchOrderResults> Get()
        {
            return OrderService.SearchOrder(Models.Request.All, "");
        }

        [HttpPost("{id}")]
        public ActionResult<List<SearchOrderDetailResults>> GetOrder(string id)
        {
            var orderDetail = OrderService.SearchOrderDetail(OrderService.Seq2Id(int.Parse(id))[0]);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return orderDetail;
        }

        [HttpPost]
        public ActionResult<int> AddOrder(OrderInfo orderInfo)
        {
            try
            {
                Helper._AddOrder(orderInfo);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return 0;
        }

        // PUT: api/Order/1
        [HttpPut("{id}")]
        public ActionResult<Order> updateOrder(string id, OrderInfo orderInfo)
        {
           
            
            
            try
            {
                int seq = int.Parse(id);

                var results = OrderService.SearchOrder(Models.Request.ID, OrderService.Seq2Id(seq)[0].ToString());
                if (results.Count == 0)
                    return BadRequest();
                OrderService.CheckSeq(orderInfo.seqNum, seq);
                OrderService.DeleteOrder(seq);
                Helper._AddOrder(orderInfo);
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrder(string id)
        {
            try
            {
                OrderService.DeleteOrder(int.Parse(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }


    }   

    
}