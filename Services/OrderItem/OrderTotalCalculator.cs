using DataBaseContent;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class OrderTotalCalculator : IOrderTotalCalculator
    {
        public ApplicationDbContext _db;

        public OrderTotalCalculator(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CalculateTotalAmountForOrder(Guid orderId, OrderItem orderItem, ActionType action)
        {
            Order? foundOrder = await _db.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (action == ActionType.Add)
                foundOrder.TotalAmount += orderItem.UnitPrice * orderItem.Quantity;

            else if (action == ActionType.Delete)
                foundOrder.TotalAmount -= orderItem.UnitPrice * orderItem.Quantity;
            else
                throw new ArgumentException("Invalid action type.");

            await _db.SaveChangesAsync();
        }

        public async Task UpdateTotalAmountForOrder(Guid orderId, OrderItem oldOrderItem, OrderItem newOrderItem)
        {
            Order? foundOrder = await _db.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);

            foundOrder.TotalAmount -= oldOrderItem.UnitPrice * oldOrderItem.Quantity;

            foundOrder.TotalAmount += newOrderItem.UnitPrice * newOrderItem.Quantity;

            await _db.SaveChangesAsync();
        }
    }
}
