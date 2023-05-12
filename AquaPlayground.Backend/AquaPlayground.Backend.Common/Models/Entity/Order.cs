﻿using AquaPlayground.Backend.Common.Enums;

namespace AquaPlayground.Backend.Common.Models.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public OrderStatus Status { get; set; }
        
        public string DeliveryAdress { get; set; }
        
        public User? User { get; set; }
        
        public Guid ServiceId { get; set; }
        
        public Service? Service { get; set; }
        
        public decimal? ServicePrice { get; set; }

        public List<OrderPromotion> OrderPromotions { get; set; }
    }
}
