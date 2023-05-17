namespace AquaPlayground.Backend.Common.Models.Entity
{
    public class Service
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string TypeService { get; set; }
        
        public decimal Price { get; set; }

        public List<OrderService> OrderServices { get; set; }

        public List<Promotion>? Promotions { get; set; }
    }
}
