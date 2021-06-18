using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        readonly long _companyId;

        public WarehouseConfiguration(){}
        public WarehouseConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouse").HasKey(w => w.Id);
            builder.Property(w => w.Id).HasColumnName("WarehouseId");

            builder.Property(p => p.Code)
               .HasMaxLength(20)
               .HasConversion(p => p.Value, p => new Code(p))
               .IsRequired();

            builder.Property(q => q.Name)
               .HasMaxLength(60)
               .HasConversion(p => p.Value, p => new Name(p))
               .IsRequired();

            builder.Property(q => q.Location)
                .HasMaxLength(60)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasMany(p => p.Products)
                .WithOne(p => p.Warehouse);

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

            builder.HasQueryFilter(x => x.CompanyId == _companyId && x.Status == EntityStatus.Active);

        }
    }
}
