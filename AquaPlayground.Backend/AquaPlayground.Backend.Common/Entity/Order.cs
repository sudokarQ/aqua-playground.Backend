using AquaPlayground.Backend.Common.Enums;

namespace AquaPlayground.Backend.Common.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotaPrice { get; set; }
        public OrderStatus Status { get; set; }
        public string DeliveryAdress { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }
        public decimal? ServicePrice { get; set; }

        public List<OrderPromotion> OrderPromotions { get; set; }
    }
}
