using DataBaseContent;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.Orders
{
    public class OrderAddRequest
    {
        public string? CustomerName { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be a positive number.")]
        public decimal TotalAmount { get; set; }

        public Order ToOrder()
        {
            return new Order() { CustomerName = CustomerName, OrderDate = OrderDate, TotalAmount = TotalAmount };
        }
    }
}
