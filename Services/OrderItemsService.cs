using ServiceContracts;
using DataBaseContent;
using ServiceContracts.DTO.OrderItems;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.Orders;

namespace Services
{
    public class OrderItemsService : IOrderItemsService
    {
        public ApplicationDbContext _db;

        public OrderItemsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OrderItemResponse?> UpdateOrderItem(Guid orderId, Guid orderItemId, OrderItemUpdateRequest? orderItemUpdateRequest)
        {
            if (orderItemUpdateRequest == null)
                return null;

            var matcingOrderItem = await _db.OrderItems.FirstOrDefaultAsync(x => x.OrderId == orderId && x.OrderItemId == orderItemId);

            if (matcingOrderItem == null)
                return null;

            OrderItem updateOrderItem = orderItemUpdateRequest.ToOrderItem();
            matcingOrderItem.OrderId = updateOrderItem.OrderId;
            matcingOrderItem.ProductName = updateOrderItem.ProductName;
            matcingOrderItem.Quantity = updateOrderItem.Quantity;
            matcingOrderItem.UnitPrice = updateOrderItem.UnitPrice;

            await _db.SaveChangesAsync();

            return matcingOrderItem.ToOrderItemResponse();

        }

        public async Task<OrderItemResponse?> AddOrderItem(Guid orderId, OrderItemAddRequest? orderItemAddRequest)
        {
            if (orderItemAddRequest == null)
                return null;

            OrderItem orderItem = orderItemAddRequest.ToOrderItem();
            orderItem.OrderId = orderId;
            orderItem.OrderItemId = Guid.NewGuid();

            _db.OrderItems.Add(orderItem);

            await _db.SaveChangesAsync();

            return orderItem.ToOrderItemResponse();
        }

        public async Task<List<OrderItemResponse?>> GetAllItemsOrder(Guid? orderId)
        {
            List<OrderItem> foundOrdersItems = await _db.OrderItems.Include("Order").ToListAsync();

            foundOrdersItems = foundOrdersItems.FindAll(x => x.OrderId == orderId);

            if (foundOrdersItems.Count == 0 || foundOrdersItems == null)
                return null;

            List<OrderItemResponse> orderItemResponses = new List<OrderItemResponse>();

            for (int i = 0; i < foundOrdersItems.Count; i++)
            {
                orderItemResponses.Add(foundOrdersItems[i].ToOrderItemResponse());
            }

            return orderItemResponses;
        }

        public async Task<OrderItemResponse?> GetOrderItemById(Guid? orderId, Guid? OrderItemId)
        {
            List<OrderItem> foundOrdersItems = await _db.OrderItems.Include("Order").ToListAsync();

            OrderItem? foundOrderItem = foundOrdersItems.FirstOrDefault(x => x.OrderId == orderId && x.OrderItemId == OrderItemId);

            if (foundOrderItem == null)
                return null;

            return foundOrderItem.ToOrderItemResponse();
        }

        public async Task<bool> DeleteOrderItem(Guid orderId, Guid orderItemId)
        {
            OrderItem? orderItem = await _db.OrderItems.FirstOrDefaultAsync(x => x.OrderId == orderId && x.OrderItemId == orderItemId);

            if (orderItem == null)
                return false;

            _db.OrderItems.Remove(orderItem);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
