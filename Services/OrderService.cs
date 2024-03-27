using DataBaseContent;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.Orders;

namespace Services
{
    public class OrderService : IOrdersService
    {
        public ApplicationDbContext _db;

        public OrderService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
        {
            Order order = orderAddRequest.ToOrder();
            
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return order.ToOrderResponse();
        }

        public async Task<List<OrderResponse>> GetAllOrders()
        {
            return await _db.Orders.Select(x => x.ToOrderResponse()).ToListAsync();
        }

        public async Task<OrderResponse?> GetOrderByOrderId(Guid? orderId)
        {
            if (orderId == null)
            {
                return null;
            }

            Order order = await _db.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (order == null)
            {
                return null;
            }

            return order.ToOrderResponse();
        }
    }
}
