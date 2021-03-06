﻿using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    class PaymentTermConfiguration : IEntityTypeConfiguration<PaymentTerm>
    {
        readonly long _companyId;

        public PaymentTermConfiguration() { }
        public PaymentTermConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }
        public void Configure(EntityTypeBuilder<PaymentTerm> builder)
        {
            builder.ToTable("PaymentTerm").HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("PaymentTermId");

            builder.Property(s => s.Description)
               .HasMaxLength(120)
               .HasConversion(s => s.Value, s => new Name(s))
               .IsRequired();

            builder.Property(s => s.DayValue)
               .IsRequired();

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

            builder.HasQueryFilter(x => x.CompanyId == _companyId 
            && x.Status == EntityStatus.Active);
        }
    }
}
