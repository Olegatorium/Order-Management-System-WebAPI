using DataBaseContent;
using ServiceContracts.DTO.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
