using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.WebAPI.Models
{
    public class OrderItem
    {
        [Key]
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

    
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }
}
