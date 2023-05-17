using AquaPlayground.Backend.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace AquaPlayground.Backend.Common.Models.Dto.Order
{
    public class OrderPostDto
    {
        [Required]
        public string DeliveryAdress { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public List<Guid> ServicesId { get; set; } = new List<Guid>();  
    }
}
