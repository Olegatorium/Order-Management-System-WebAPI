using DataBaseContent;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.Orders
{
    public class OrderUpdateRequest
    {
        [Required]
        public Guid OrderId { get; set; }

        public int OrderCounter { get; set; }

        public string? OrderNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string? CustomerName { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be a positive number.")]
        public decimal TotalAmount { get; set; }

        public Order ToOrder()
        {
            return new Order()
            {
                OrderId = OrderId, OrderNumber = OrderNumber, CustomerName = CustomerName,
                OrderDate = OrderDate, TotalAmount = TotalAmount
            };
        }
    }
}
