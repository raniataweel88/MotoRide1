﻿namespace MotoRide.Models
{
    public class OrderItem
    {

        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? MotorcycleId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public DateTime CreatedAt { get; set; }
        public Product? Product { get; set; }
        public Motorcycle? Motorcycle { get; set; }

    }

    
}
