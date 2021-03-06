﻿using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        readonly long _companyId;

        public InvoiceDetailConfiguration() { }
        public InvoiceDetailConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable("InvoiceDetail").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("InvoiceDetailId");

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

            builder.Property(q => q.Amount)
                .HasColumnType("decimal(16,2)");

            builder.HasOne(c => c.InvoiceHeader);

            builder.HasQueryFilter(x => x.CompanyId == _companyId
            && x.Status == EntityStatus.Active);
        }
    }
}
