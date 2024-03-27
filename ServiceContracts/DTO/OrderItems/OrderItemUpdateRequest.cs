
using DataBaseContent;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.OrderItems
{
    public class OrderItemUpdateRequest
    {
        [Required]
        public Guid OrderItemId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ProductName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "UnitPrice must be a positive number.")]
        public decimal UnitPrice { get; set; }

        public OrderItem ToOrderItem()
        {
            return new OrderItem()
            {
                OrderItemId = OrderItemId,
                OrderId = OrderId,
                ProductName = ProductName,
                Quantity = Quantity,
                UnitPrice = UnitPrice
            };
        }
    }
}
