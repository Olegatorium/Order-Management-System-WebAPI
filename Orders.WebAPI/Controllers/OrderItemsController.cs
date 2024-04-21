using DataBaseContent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.OrderItems;
using ServiceContracts.DTO.Orders;
using Services;

namespace Orders.WebAPI.Controllers
{
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

        // GET:api/orders/orderId/items/Id
        [HttpGet("{orderId}/items/{OrderItemId}")]
        public async Task<ActionResult<OrderItemResponse>> GetOrderItem(Guid? orderId, Guid? OrderItemId) 
        {
            var matchingOrderItem = await _orderItemsService.GetOrderItemById(orderId, OrderItemId);

            if (matchingOrderItem == null)
                return Problem("Matching Order Item not found", statusCode: 404, title: "Order Items Search By Id");

            return matchingOrderItem;
        }

        // Post:api/orders/orderId/items
        [HttpPost("{orderId}/items")]
        public async Task<ActionResult<OrderItemResponse>> AddOrderItem(Guid orderId, [FromBody]OrderItemAddRequest orderItemAddRequest) 
        {
            OrderItemResponse? addedOrderItem = await _orderItemsService.AddOrderItem(orderId, orderItemAddRequest);

            if (addedOrderItem == null)
               return Problem("Can`t add new order item", statusCode: 400, title: "Add Order Item");

            return CreatedAtAction("GetOrderItem", new { orderId = addedOrderItem.OrderId, OrderItemId = addedOrderItem.OrderItemId }, addedOrderItem);
        }
    }
}
