using ServiceContracts;
using DataBaseContent;
using ServiceContracts.DTO.OrderItems;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class OrderItemsService : IOrderItemsService
    {
        public ApplicationDbContext _db;

        public OrderItemsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<OrderItemResponse?>> GetAllItemsOrder(Guid? orderId)
        {
            var foundOrdersItems = await _db.OrderItems.Include("Order").ToListAsync();

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
    }
}
