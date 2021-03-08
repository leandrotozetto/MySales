using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySales.Product.Api.Domain.Core.Enum;
using MySales.Product.Api.Domain.Identifiers;

namespace MySales.Product.Api.Infrastructure.Configurations
{
    class SkuConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Sku>
    {
        public static SkuConfiguration New()
        {
            return new SkuConfiguration();
        }

        public void Configure(EntityTypeBuilder<Domain.Aggregates.Sku> builder)
        {
            builder.HasKey(x=> x.SkuId);

            builder.Property(x=> x.SkuId)
                .HasConversion(x=> x.Value, x=> SkuId.New(x));

            builder.Property(x => x.TenantId)
                .HasConversion(x => x.Value, x => Domain.Core.Entities.Identifiers.TenantId.New(x))
                .IsRequired();

            builder.Property(x => x.SizeId)
                .HasConversion(x => x.Value, x => SizeId.New(x))
                .IsRequired();

            builder.Property(x => x.ProductId)
                .HasConversion(x => x.Value, x => ProductId.New(x))
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion(x => x.Value, x => StatusEnum.New(x))
                .IsRequired();

            builder.Property(x => x.Stock)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Description);

            builder.Property(x => x.UpdateDate);

            builder.HasIndex("PartNumber", "ProductId")
                .IsUnique();
        }
    }
}
