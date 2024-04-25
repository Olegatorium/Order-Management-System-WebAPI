using DataBaseContent;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.Orders
{
    public class OrderAddRequest
    {
        [Required]
        [MaxLength(50)]
        public string? CustomerName { get; set; }

        public Order ToOrder()
        {
            return new Order() { CustomerName = CustomerName, OrderDate = DateTime.Now };
        }
    }
}
