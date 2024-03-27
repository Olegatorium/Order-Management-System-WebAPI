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

        public async Task<List<OrderResponse>> GetAllOrders()
        {
            return await _db.Orders.Select(x => x.ToOrderResponse()).ToListAsync();
        }
    }
}
