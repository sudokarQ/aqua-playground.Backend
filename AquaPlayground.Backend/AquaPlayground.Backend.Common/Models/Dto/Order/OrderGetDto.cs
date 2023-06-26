namespace AquaPlayground.Backend.Common.Models.Dto.Order
{
    using Enums;

    using Service;

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
