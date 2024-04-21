using ServiceContracts.DTO.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IOrderItemsService
    {
        Task<List<OrderItemResponse?>> GetAllItemsOrder(Guid? orderId);

        //Retrieve an order item by ID
        Task<OrderItemResponse?> GetOrderItemById(Guid? orderId, Guid? OrderItemId);

        //Add a new order item
        Task<OrderItemResponse?> AddOrderItem(Guid orderId, OrderItemAddRequest? orderItemAddRequest);
    }
}
