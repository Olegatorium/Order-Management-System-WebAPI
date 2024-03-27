using ServiceContracts.DTO.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IOrdersService
    {
        Task<List<OrderResponse>> GetAllOrders();
    }
}
