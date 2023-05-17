namespace AquaPlayground.Backend.Common.Models.Entity
{
    public class OrderService
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        
        public Guid ServiceId { get; set; }


        public Order Order { get; set; }
        
        public Service Service { get; set; }
    }
}
