namespace AquaPlayground.Backend.Common.Models.Dto.Promotion
{
    public class PromotionGetDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime? BeginDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int DiscountPercent { get; set; }
        
        public Guid? ServiceId { get; set; }
    }
}
