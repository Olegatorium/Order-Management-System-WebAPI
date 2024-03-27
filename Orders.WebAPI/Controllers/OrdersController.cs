using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.Orders;

namespace Orders.WebAPI.Controllers
{
    public class OrdersController : CustomControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IOrderItemsService _orderItemsService;

        public OrdersController(IOrdersService ordersService, IOrderItemsService orderItemsService)
        {
            _ordersService = ordersService;
            _orderItemsService = orderItemsService;
        }


        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
        {
            return await _ordersService.GetAllOrders();
        }

    }
}
