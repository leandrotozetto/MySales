using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySales.Product.Api.Domain.Core.Entities.Identifiers;
using MySales.Product.Api.Domain.Core.Enum;
using MySales.Product.Api.Domain.Identifiers;

namespace MySales.Product.Api.Infrastructure.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Product>
    {
        public static ProductConfiguration New()
        {
            return new ProductConfiguration();
        }

        public void Configure(EntityTypeBuilder<Domain.Aggregates.Product> builder)
        {
            builder.HasKey(x=> x.ProductId);

            builder.Property(x=> x.ProductId)
                .HasConversion(x=> x.Value, x=> ProductId.New(x));

            builder.Property(x => x.TenantId)
                .HasConversion(x => x.Value, x => TenantId.New(x))
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion(x => x.Value, x => StatusEnum.New(x))
                .IsRequired();

            builder.Property(x => x.CreationDate)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.UpdateDate);

            builder.HasMany(x => x.Skus).WithOne().HasForeignKey("ProductId");
        }
    }
}
