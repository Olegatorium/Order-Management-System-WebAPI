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
    }
}
