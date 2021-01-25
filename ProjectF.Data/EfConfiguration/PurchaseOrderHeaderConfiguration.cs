using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PurchaseOrders;

namespace ProjectF.Data.EfConfiguration
{
    class PurchaseOrderHeaderConfiguration : IEntityTypeConfiguration<PurchaseOrderHeader>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderHeader> builder)
        {
            builder.ToTable("PurchaseOrderHeader").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("PurchaseOrderHeaderId");

            builder.Property(c => c.Code)
               .HasMaxLength(20)
               .HasConversion(c => c.Value, c => new Code(c))
               .IsRequired();

            builder.Property(c => c.SupplierName)
                .HasMaxLength(60)
                .HasConversion(c => c.Value, c => new Name(c))
                .IsRequired();

            builder.Property(c => c.SupplierId)
                .IsRequired();
            
            builder.Property(c => c.Rnc);

            builder.Property(q => q.Address)
                .HasMaxLength(220)
                .HasConversion(p => p.Value, p => new GeneralText(p));

            builder.Property(q => q.DeliverDate)
             .HasColumnType("Datetime");

            builder.Property(c => c.PaymentTermName)
               .HasMaxLength(60)
               .HasConversion(c => c.Value, c => new Name(c));

            builder.Property(c => c.PaymentTermId);

            builder.Property(c => c.WarehouseName)
               .HasMaxLength(60)
               .HasConversion(c => c.Value, c => new Name(c));

            builder.Property(c => c.WarehouseId);

            builder.Property(q => q.Notes)
                .HasMaxLength(220)
                .HasConversion(p => p.Value, p => new GeneralText(p));

            builder.Property(q => q.DiscountTotal)
             .HasColumnType("decimal(12,2)");

            builder.Property(q => q.Subtotal)
             .HasColumnType("decimal(12,2)");

            builder.Property(q => q.TaxTotal)
             .HasColumnType("decimal(12,2)");

            builder.Property(q => q.Total)
             .HasColumnType("decimal(12,2)");


            builder.HasMany(q => q.PurchaseDetails).WithOne(q => q.PurchaseOrderHeader);

            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(q => q.Created)
                .HasColumnType("Datetime")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(q => q.Modified)
                .HasColumnType("Datetime");


        }
    }
}
