using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PurchaseOrders;

namespace ProjectF.Data.EfConfiguration
{
    class PurchaseOrderDetailConfiguration : IEntityTypeConfiguration<PurchaseOrderDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDetail> builder)
        {
            builder.ToTable("PurchaseOrderDetail").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("PurchaseOrderDetailId");


            builder.Property(c => c.ProductCode)
           .HasMaxLength(20)
           .HasConversion(c => c.Value, c => new Code(c))
           .IsRequired();

            builder.Property(c => c.Description)
           .HasMaxLength(60)
           .HasConversion(c => c.Value, c => new Name(c))
           .IsRequired();


            builder.Property(q => q.Qty)
             .HasColumnType("int");

            builder.Property(q => q.TaxPercent)
                .HasColumnType("decimal(6,2)");

            builder.Property(q => q.Cost)
              .HasColumnType("decimal(16,2)");

            builder.Property(q => q.DiscountValue)
                .HasColumnType("decimal(12,2)");

            builder.HasOne(c => c.PurchaseOrderHeader);

        }
    }
}
