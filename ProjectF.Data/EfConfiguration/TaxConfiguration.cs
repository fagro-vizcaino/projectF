using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    class TaxConfiguration : IEntityTypeConfiguration<Tax>
    {
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

        }
    }
}
