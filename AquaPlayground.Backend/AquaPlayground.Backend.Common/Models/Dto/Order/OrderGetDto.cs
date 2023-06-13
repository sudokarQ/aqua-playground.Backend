using AquaPlayground.Backend.Common.Enums;
using AquaPlayground.Backend.Common.Models.Dto.Service;

namespace AquaPlayground.Backend.Common.Models.Dto.Order
{
    public class OrderGetDto
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public string DeliveryAdress { get; set; }

        public DateTime DateTime { get; set; }

        public OrderStatus Status { get; set; }

        public List<ServiceSearchGetDto> Services { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
