using System;
using System.Collections.Generic;
using System.Text;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Suppliers;
using ProjectF.Data.Entities.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        readonly long _companyId;

        public SupplierConfiguration(){}
        public SupplierConfiguration(long companyId): this()
        {
            _companyId = companyId;
        }

        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier").HasKey(s => s.Id)
                .IsClustered();
            builder.Property(s => s.Id).HasColumnName("SupplierId");

            builder.Property(s => s.Code)
               .HasMaxLength(20)
               .HasConversion(s => s.Value, s => new Code(s))
               .IsRequired();

            builder.Property(s => s.Name)
               .HasMaxLength(60)
               .HasConversion(s => s.Value, s => new Name(s))
               .IsRequired();

            builder.Property(s => s.Email)
                .HasMaxLength(60)
                .HasConversion(s => s.Value, s => new Email(s))
                .IsRequired();

            builder.Property(s => s.Phone)
                .HasMaxLength(11)
                .HasConversion(s => s.Value, s => new Phone(s))
                .IsRequired();

            builder.Property(s => s.Rnc)
                .HasMaxLength(15);

            builder.HasOne(p => p.Country)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(s => s.HomeOrApartment)
                .HasMaxLength(200);

            builder.Property(s => s.City)
                .HasMaxLength(60);

            builder.Property(s => s.Street)
                .HasMaxLength(60);

            builder.Property(s => s.IsInformalSupplier);

            builder.Property(c => c.Notes)
                .HasMaxLength(220)
                .HasConversion(c => c.Value, c => new GeneralText(c));

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
