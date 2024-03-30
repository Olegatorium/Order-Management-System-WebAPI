using DataBaseContent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.OrderItems;
using ServiceContracts.DTO.Orders;

namespace Orders.WebAPI.Controllers
{
    [Route("api/[controller]")]

    public class OrdersController : CustomControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        // GET:api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
        {
            return await _ordersService.GetAllOrders();
        }

        // GET: api/Orders/5
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(Guid? orderId)
        {
            var foundOrder = await _ordersService.GetOrderByOrderId(orderId);

            if (foundOrder == null)
            {
                return Problem("Order not found", statusCode: 404, title: "Order Search");
            }

            return foundOrder;
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> PostOrder(OrderAddRequest? orderAddRequest)
        {
            OrderResponse? orderResponse = await _ordersService.AddOrder(orderAddRequest);

            if (orderResponse == null)
            {
                return Problem("Can`t add order", statusCode: 400, title: "Add Order");
            }

            return CreatedAtAction("GetOrder", new { orderId = orderResponse.OrderId }, orderResponse);
        }

        //PUT: api/Orders/5
        [HttpPut("{orderId}")]
        public async Task<IActionResult> PutOrder(Guid orderId, OrderUpdateRequest orderUpdateRequest) 
        {
            OrderResponse? orderResponse = await _ordersService.UpdateOrder(orderId, orderUpdateRequest);

            if (orderResponse == null)
            {
                return Problem("Can`t update order", statusCode: 400, title: "Update Order");
            }

            return CreatedAtAction("GetOrder", new { orderId = orderResponse.OrderId }, orderResponse);
        }

        //Delete: api/Orders/5
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<OrderResponse>> DeleteOrder(Guid? orderId) 
        {
            var IsRemoved = await _ordersService.DeleteOrder(orderId);

            if (!IsRemoved)
                return Problem("Order not found", statusCode: 400, title: "Delete Order");

            return NoContent();
        }

    }
}
