using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    public class TaxConfiguration : IEntityTypeConfiguration<Tax>
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
