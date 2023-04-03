namespace AquaPlayground.Backend.Common.Entity
{
    public class Promotion
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? DiscountPercent { get; set; }
        public Guid? ServiceId { get; set; }
        public Service? Service { get; set; }

        public List<OrderPromotion> OrderPromotions { get; set; }
    }
}
