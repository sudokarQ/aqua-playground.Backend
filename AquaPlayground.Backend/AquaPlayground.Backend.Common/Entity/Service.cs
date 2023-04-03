namespace AquaPlayground.Backend.Common.Entity
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TypeService { get; set; }
        public decimal Price { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Promotion>? Promotions { get; set; }
    }
}
