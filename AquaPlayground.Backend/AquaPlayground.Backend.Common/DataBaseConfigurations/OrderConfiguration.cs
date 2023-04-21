using AquaPlayground.Backend.Common.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaPlayground.Backend.Common.DataBaseConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.OrderPromotions);

            builder.HasOne(t => t.Service);
            builder.HasOne(t => t.User);
        }
    }
}
