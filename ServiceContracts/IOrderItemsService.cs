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
        // Get All Order Items
        Task<List<OrderItemResponse?>> GetAllItemsOrder(Guid? orderId);

        //Retrieve an order item by ID
        Task<OrderItemResponse?> GetOrderItemById(Guid? orderId, Guid? orderItemId);

        //Add a new order item
        Task<OrderItemResponse?> AddOrderItem(Guid orderId, OrderItemAddRequest? orderItemAddRequest);

        //Update an existing Order Item
        Task<OrderItemResponse?> UpdateOrderItem(Guid orderId, Guid orderItemId, OrderItemUpdateRequest? orderItemUpdateRequest);

        // Remove an existing order item
        Task<bool> DeleteOrderItem(Guid orderId, Guid orderItemId);
    }
}
