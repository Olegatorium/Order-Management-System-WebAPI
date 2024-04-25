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
        private readonly IOrdersService _ordersService;

        public OrderItemsController(IOrderItemsService orderItemsService, IOrdersService ordersService)
        {
            _orderItemsService = orderItemsService;
            _ordersService = ordersService;
        }

        // GET:api/orders/orderId/items
        [HttpGet("{orderId}/items")]
        public async Task<ActionResult<IList<OrderItemResponse?>>> GetOrderItems(Guid orderId)
        {
            var matchingItems = await _orderItemsService.GetAllItemsOrder(orderId);

            if (matchingItems == null)
                return Problem("Matching Items not found", statusCode: 400, title: "Order Items Search");

            return matchingItems;
        }

        // GET:api/orders/orderId/items/Id
        [HttpGet("{orderId}/items/{orderItemId}")]
        public async Task<ActionResult<OrderItemResponse>> GetOrderItem(Guid? orderId, Guid? orderItemId)
        {
            var matchingOrderItem = await _orderItemsService.GetOrderItemById(orderId, orderItemId);

            if (matchingOrderItem == null)
                return Problem("Matching Order Item not found", statusCode: 404, title: "Order Items Search By Id");

            return matchingOrderItem;
        }

        // Post:api/orders/orderId/items
        [HttpPost("{orderId}/items")]
        public async Task<ActionResult<OrderItemResponse>> AddOrderItem(Guid orderId, [FromBody] OrderItemAddRequest orderItemAddRequest)
        {
            if (!await _ordersService.IsOrderExist(orderId))
                return Problem("No orders for the specified order ID", statusCode: 404, title: "Update Order Item");

            OrderItemResponse? addedOrderItem = await _orderItemsService.AddOrderItem(orderId, orderItemAddRequest);

            if (addedOrderItem == null)
                return Problem("Order Item Request can`t be blank", statusCode: 400, title: "Add Order Item");

            return CreatedAtAction("GetOrderItem", new { orderId = addedOrderItem.OrderId, OrderItemId = addedOrderItem.OrderItemId }, addedOrderItem);
        }

        // PUT:/api/orders/{orderId}/items/{id}
        [HttpPut("{orderId}/items/{orderItemId}")]
        public async Task<ActionResult<OrderItemResponse>> UpdateOrderItem(Guid orderId, Guid orderItemId, [FromBody] OrderItemUpdateRequest orderItemUpdateRequest)
        {
            if (!await _ordersService.IsOrderExist(orderItemUpdateRequest.OrderId) || !await _ordersService.IsOrderExist(orderId))
                return Problem("No orders for the specified order ID", statusCode: 404, title: "Update Order Item");

            if (!await _orderItemsService.IsOrderItemExist(orderId,orderItemId))
                return Problem("No order items for the specified order item ID", statusCode: 404, title: "Update Order Item");

            OrderItemResponse? updatedOrderItem = await _orderItemsService.UpdateOrderItem(orderId, orderItemId, orderItemUpdateRequest);

            if (updatedOrderItem == null)
                return Problem("Order Item Update Request can`t be blank", statusCode: 400, title: "Update Order Item");

            return CreatedAtAction("GetOrderItem", new { orderId = updatedOrderItem.OrderId, OrderItemId = updatedOrderItem.OrderItemId }, updatedOrderItem);
        }

        // Delete:/api/orders/{orderId}/items/{id}
        [HttpDelete("{orderId}/items/{orderItemId}")]
        public async Task <IActionResult> DeleteOrderItem(Guid orderId, Guid orderItemId)
        {
            bool IsDeleted =  await _orderItemsService.DeleteOrderItem(orderId, orderItemId);

            if (!IsDeleted)
                return Problem("Order item not found", statusCode: 404, title: "Remove Order Item");

            return NoContent();
        }
    }
}
