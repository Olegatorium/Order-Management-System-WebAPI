using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseContent
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Editable(false)]
        public string? OrderNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string? CustomerName { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be a positive number.")]
        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderItem>? OrderItem { get; set; }
    }
}
