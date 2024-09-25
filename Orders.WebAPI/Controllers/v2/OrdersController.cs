using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.Orders;

namespace Orders.WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    public class OrdersController : CustomControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        // GET:api/Orders
        /// <summary>
        /// To get list of only orders id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Produces("application/xml")]
        public async Task<ActionResult<IEnumerable<Guid>>> GetOrders()
        {
            var orders = await _ordersService.GetAllOrders();

            var res = orders.Select(x => x.OrderId).ToList();

            if(orders == null || orders.Count == 0)
                return Problem("Orders not found", statusCode: 400, title: "Get Orders");

            return res;
        }

        // GET: api/Orders/5
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(Guid? orderId)
        {
            var foundOrder = await _ordersService.GetOrderByOrderId(orderId);

            if (foundOrder == null)
                return Problem("Order not found", statusCode: 404, title: "Order Search");

            return foundOrder;
        }
    }
}
