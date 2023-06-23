namespace AquaPlayground.Backend.Common.Models.Dto.Order
{
    using System.ComponentModel.DataAnnotations;

    using Enums;

    public class OrderPutDto
    {
        [Required]
        public Guid Id { get; set; }

        public OrderStatus? Status { get; set; }

        [Range(0, int.MaxValue)]
        public decimal? TotalPrice { get; set; }

        public DateTime? DateTime { get; set; }
    }
}
