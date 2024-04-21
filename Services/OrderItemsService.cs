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

        public async Task<OrderItemResponse?> AddOrderItem(Guid orderId, OrderItemAddRequest? orderItemAddRequest)
        {
            if (orderItemAddRequest == null)
                return null;

            var matcingOrder = await _db.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (matcingOrder == null)
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
    }
}
