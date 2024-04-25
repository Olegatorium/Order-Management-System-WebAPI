
using ServiceContracts.DTO.Orders;


namespace ServiceContracts
{
    public interface IOrdersService
    {
        Task<List<OrderResponse>> GetAllOrders();

        Task<OrderResponse?> GetOrderByOrderId(Guid? orderId);

        Task<OrderResponse?> AddOrder(OrderAddRequest? orderAddRequest);

        Task<string?> GetNewOrderNumber();

        Task<OrderResponse?> UpdateOrder(Guid? orderId, OrderUpdateRequest? orderUpdateRequest);

        Task<bool> DeleteOrder(Guid? orderId);

        Task<bool> IsOrderExist(Guid orderId);


    }
}
