using AquaPlayground.Backend.Common.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaPlayground.Backend.Common.DataBaseConfigurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("services");

            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.Promotions);

            builder.HasMany(t => t.Orders);

            builder
                .HasMany(s => s.Promotions)
                .WithOne(p => p.Service)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(s => s.Orders)
                .WithOne(p => p.Service)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
