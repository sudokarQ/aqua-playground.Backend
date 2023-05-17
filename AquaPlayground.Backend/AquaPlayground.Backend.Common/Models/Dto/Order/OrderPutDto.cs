using AquaPlayground.Backend.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace AquaPlayground.Backend.Common.Models.Dto.Order
{
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
