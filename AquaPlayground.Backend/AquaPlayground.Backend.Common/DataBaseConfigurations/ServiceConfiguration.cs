using AquaPlayground.Backend.Common.Entity;
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
        }
    }
}
