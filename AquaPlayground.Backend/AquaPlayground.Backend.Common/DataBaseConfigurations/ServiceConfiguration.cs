namespace AquaPlayground.Backend.Common.DataBaseConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Entity;

    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("services");

            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.Promotions);

            builder.HasMany(t => t.OrderServices);

            builder
                .HasMany(s => s.Promotions)
                .WithOne(p => p.Service)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
