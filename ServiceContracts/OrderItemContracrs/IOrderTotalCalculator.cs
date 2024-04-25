using DataBaseContent;

namespace ServiceContracts
{
    public interface IOrderTotalCalculator
    {
        Task CalculateTotalAmountForOrder(Guid orderId, OrderItem orderItem, ActionType action);
        Task UpdateTotalAmountForOrder(Guid orderId, OrderItem oldOrderItem, OrderItem newOrderItem);
    }
}
