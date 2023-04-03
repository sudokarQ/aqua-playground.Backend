using AquaPlayground.Backend.Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaPlayground.Backend.Common.DataBaseConfigurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("promotions");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Service);
        }
    }
}
