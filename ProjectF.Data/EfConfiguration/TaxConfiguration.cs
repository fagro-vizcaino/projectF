using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    class TaxConfiguration : IEntityTypeConfiguration<Tax>
    {
        readonly long _companyId;

        public TaxConfiguration() { }
        public TaxConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }

        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.ToTable("Tax").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("TaxId");

            builder.Property(q => q.Name)
                .HasMaxLength(60)
                .HasConversion(p => p.Value, p => new Name(p))
                .IsRequired();

            builder.Property(q => q.PercentValue)
                .HasColumnType("decimal(12,2)")
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
