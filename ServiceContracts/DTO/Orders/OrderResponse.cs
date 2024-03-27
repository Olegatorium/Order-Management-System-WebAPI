using Orders.WebAPI.Models;
using ServiceContracts.DTO.OrderItems;

namespace ServiceContracts.DTO.Orders
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }

        public int OrderCounter { get; set; }

        public string? OrderNumber { get; set; }

        public string? CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderUpdateRequest ToOrderUpdateRequest()
        {
            return new OrderUpdateRequest()
            {
                OrderId = OrderId, OrderCounter = OrderCounter, OrderNumber = OrderNumber,
                CustomerName = CustomerName, OrderDate = OrderDate , TotalAmount = TotalAmount
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
                OrderCounter = order.OrderCounter,
                CustomerName = order.CustomerName,
                TotalAmount = order.TotalAmount
            };
        }
    }
}
