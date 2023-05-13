using System.ComponentModel.DataAnnotations;

namespace AquaPlayground.Backend.Common.Models.Dto.Promotion
{
    public class PromotionPostDto : IPromotionDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, 100)]
        public int? DiscountPercent { get; set; }

        [Required]
        public DateTime? BeginDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public Guid? ServiceId { get; set; }
    }
}
