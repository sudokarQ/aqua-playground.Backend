namespace AquaPlayground.Backend.Common.DataBaseConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Entity;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.OrderPromotions);

            builder.HasMany(t => t.OrderServices);

            builder.HasOne(t => t.User);
        }
    }
}
