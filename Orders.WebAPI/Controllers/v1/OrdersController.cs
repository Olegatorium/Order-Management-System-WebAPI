using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.Orders;

namespace Orders.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class OrdersController : CustomControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        // GET:api/Orders
        /// <summary>
        /// To get list of orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Produces("application/xml")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
        {
            var orders = await _ordersService.GetAllOrders();

            if (orders == null || orders.Count == 0)
                return Problem("Orders not found", statusCode: 400, title: "Get Orders");

            return orders;
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

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> PostOrder(OrderAddRequest? orderAddRequest)
        {
            OrderResponse? orderResponse = await _ordersService.AddOrder(orderAddRequest);

            if (orderResponse == null)
                return Problem("Order Request can`t be blank", statusCode: 400, title: "Add Order");

            return CreatedAtAction("GetOrder", new { orderId = orderResponse.OrderId }, orderResponse);
        }

        //PUT: api/Orders/5
        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderResponse>> PutOrder(Guid orderId, OrderUpdateRequest orderUpdateRequest)
        {
            if (!await _ordersService.IsOrderExist(orderId))
                return Problem("No orders for the specified order ID", statusCode: 404, title: "Update Order");

            OrderResponse? orderResponse = await _ordersService.UpdateOrder(orderId, orderUpdateRequest);

            if (orderResponse == null)
                return Problem("Order Update Request can`t be blank", statusCode: 400, title: "Update Order");

            return CreatedAtAction("GetOrder", new { orderId = orderResponse.OrderId }, orderResponse);
        }

        //Delete: api/Orders/5
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid? orderId)
        {
            bool IsDeleted = await _ordersService.DeleteOrder(orderId);

            if (!IsDeleted)
                return Problem("Order not found", statusCode: 404, title: "Remove Order");

            return NoContent();
        }
    }
}
