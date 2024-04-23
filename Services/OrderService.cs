using DataBaseContent;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.OrderItems;
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
        public async Task<bool> IsOrderExist(Guid orderId)
        {
            if (await _db.Orders.AnyAsync(x => x.OrderId == orderId))
                return true;

            return false;
        }

        public async Task<string?> GetNewOrderNumber() 
        {
            var latestOrder = await _db.Orders.OrderByDescending(row => row.OrderDate).FirstOrDefaultAsync();

            if (latestOrder == null)
                return null; 

            string latestOrderNumber = latestOrder.OrderNumber;

            int counter = 0;
            string orders = "";

            for (int i = 11; i < latestOrderNumber.Length; i++)
            {
                if (!char.IsDigit(latestOrderNumber[i]))
                    break;

                orders += latestOrderNumber[i];
            }

            counter = int.Parse(orders);

            counter++;

            int currYear = DateTime.Now.Year;
            orders = counter.ToString();

            string orderNumber = $"Order_{currYear}_{orders}";

            return orderNumber;
        }

        public async Task<OrderResponse?> AddOrder(OrderAddRequest? orderAddRequest)
        {
            if (orderAddRequest == null)
                return null;

            Order order = orderAddRequest.ToOrder();
            order.OrderId = Guid.NewGuid();
            order.OrderNumber = await GetNewOrderNumber();
     
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return order.ToOrderResponse();
        }

        public async Task<List<OrderResponse>> GetAllOrders()
        {
            return await _db.Orders.OrderByDescending(x => x.OrderDate).Select(x => x.ToOrderResponse()).ToListAsync();
        }

        public async Task<OrderResponse?> GetOrderByOrderId(Guid? orderId)
        {
            if (orderId == null)
                return null;

            Order? order = await _db.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (order == null)
                return null;

            return order.ToOrderResponse();
        }

        public async Task<OrderResponse?> UpdateOrder(Guid? orderId, OrderUpdateRequest? orderUpdateRequest)
        {
            if (orderUpdateRequest == null)
                return null;

            var foundOrder = await _db.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (foundOrder == null)
                return null;

            Order UpdatedOrder = orderUpdateRequest.ToOrder();

            foundOrder.CustomerName = UpdatedOrder.CustomerName;
            foundOrder.TotalAmount = UpdatedOrder.TotalAmount;

            await _db.SaveChangesAsync();

            return foundOrder.ToOrderResponse();
        }

        public async Task<bool> DeleteOrder(Guid? orderId)
        {
            var foundOrder = await _db.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (foundOrder == null)
                return false;

            _db.Orders.Remove(foundOrder);
            await _db.SaveChangesAsync();
            return true;

        }       
    }
}
