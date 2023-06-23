namespace AquaPlayground.Backend.Common.Models.Dto.Promotion
{
    using System.ComponentModel.DataAnnotations;

    public class PromotionPutDto : IPromotionDto
    {
        [Required]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Range(0, 100)]
        public int? DiscountPercent { get; set; }
    }
}
