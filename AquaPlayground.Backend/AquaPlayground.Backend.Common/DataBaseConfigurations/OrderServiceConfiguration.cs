using AquaPlayground.Backend.Common.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class OrderServiceConfiguration : IEntityTypeConfiguration<OrderService>
{
    public void Configure(EntityTypeBuilder<OrderService> builder)
    {
        builder.ToTable("order_services");

        builder.HasKey(o => o.Id);

        builder.HasOne(os => os.Order)
            .WithMany(o => o.OrderServices)
            .HasForeignKey(os => os.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(os => os.Service)
            .WithMany(s => s.OrderServices)
            .HasForeignKey(os => os.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
