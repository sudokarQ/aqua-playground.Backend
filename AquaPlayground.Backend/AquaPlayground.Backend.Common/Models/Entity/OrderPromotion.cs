﻿namespace AquaPlayground.Backend.Common.Models.Entity
{
    public class OrderPromotion
    {
        public Guid OrderId { get; set; }
        
        public Guid PromotionId { get; set; }

        public Order Order { get; set; }
        
        public Promotion Promotion { get; set; }
    }
}
