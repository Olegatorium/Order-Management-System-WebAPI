using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.OrderItems;
using ServiceContracts.DTO.Orders;
using Services;

namespace Orders.WebAPI.Controllers
{
    [Route("api/Orders")]

    public class OrderItemsController : CustomControllerBase
    {
        private readonly IOrderItemsService _orderItemsService;

        public OrderItemsController(IOrderItemsService orderItemsService)
        {
            _orderItemsService = orderItemsService;
        }

        // GET:api/orders/orderId/items
        [HttpGet("{orderId}/items")]
        public async Task<ActionResult<IList<OrderItemResponse?>>> GetOrderItems(Guid orderId)
        {
            var matchingItems = await _orderItemsService.GetAllItemsOrder(orderId);

            if (matchingItems == null)
                return Problem("Matching Items not found", statusCode: 404, title: "Order Items Search");

            return matchingItems;
        }
    }
}
