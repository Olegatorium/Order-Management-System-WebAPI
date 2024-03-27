
using DataBaseContent;

namespace ServiceContracts.DTO.OrderItems
{
    public class OrderItemResponse
    {
        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }

        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderItemUpdateRequest ToOrderItemUpdateRequest()
        {
            return new OrderItemUpdateRequest()
            {
                OrderItemId = OrderItemId,
                OrderId = OrderId,
                ProductName = ProductName,
                Quantity = Quantity,
                UnitPrice = UnitPrice
            };
        }

    }

    public static class OrderItemExtensions
    {
        public static OrderItemResponse ToOrderItemResponse(this OrderItem orderItem)
        {
            return new OrderItemResponse()
            {
                OrderItemId = orderItem.OrderItemId,
                OrderId = orderItem.OrderId,
                ProductName = orderItem.ProductName,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                TotalPrice = orderItem.Quantity * orderItem.UnitPrice
            };
        }
    }
}
