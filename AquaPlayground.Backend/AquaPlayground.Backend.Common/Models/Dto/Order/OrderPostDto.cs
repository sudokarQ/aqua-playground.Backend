namespace AquaPlayground.Backend.Common.Models.Dto.Order
{
    using System.ComponentModel.DataAnnotations;

    using Enums;

    public class OrderPostDto
    {
        [Required]
        public string DeliveryAdress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public List<Guid> ServicesId { get; set; } = new List<Guid>();
    }
}
