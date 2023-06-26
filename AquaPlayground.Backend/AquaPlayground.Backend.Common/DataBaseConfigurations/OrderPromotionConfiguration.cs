namespace AquaPlayground.Backend.Common.DataBaseConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Entity;

    public class OrderPromotionConfiguration : IEntityTypeConfiguration<OrderPromotion>
    {
        public void Configure(EntityTypeBuilder<OrderPromotion> builder)
        {
            builder.ToTable("order_promotion");

            builder.HasKey(t => new { t.OrderId, t.PromotionId });

            builder.HasOne(t => t.Order);
            builder.HasOne(t => t.Promotion);
        }
    }
}
