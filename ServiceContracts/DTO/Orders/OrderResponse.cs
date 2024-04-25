using DataBaseContent;
using ServiceContracts.DTO.OrderItems;

namespace ServiceContracts.DTO.Orders
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }

        public string? OrderNumber { get; set; }

        public string? CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderUpdateRequest ToOrderUpdateRequest()
        {
            return new OrderUpdateRequest()
            {
                CustomerName = CustomerName,
            };
        }
    }

    public static class OrderExtensions
    {
        public static OrderResponse ToOrderResponse(this Order order)
        {
            return new OrderResponse()
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                CustomerName = order.CustomerName,
                TotalAmount = order.TotalAmount
            };
        }
    }
}
