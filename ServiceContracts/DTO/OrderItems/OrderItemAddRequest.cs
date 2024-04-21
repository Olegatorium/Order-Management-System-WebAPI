using DataBaseContent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.OrderItems
{
    public class OrderItemAddRequest
    {
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
                ProductName = ProductName,
                Quantity = Quantity,
                UnitPrice = UnitPrice
            };
        }
    }
}
