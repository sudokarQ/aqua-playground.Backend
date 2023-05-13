namespace AquaPlayground.Backend.Common.Models.Dto.Promotion
{
    public interface IPromotionDto
    {
        DateTime? BeginDate { get; set; }
        
        DateTime? EndDate { get; set; }
        
        int? DiscountPercent { get; set; }
    }
}
