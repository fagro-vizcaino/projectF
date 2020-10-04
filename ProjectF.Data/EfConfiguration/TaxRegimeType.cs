using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    public class TaxRegimeTypeConfiguration : IEntityTypeConfiguration<TaxRegimeType>
    {
        public void Configure(EntityTypeBuilder<TaxRegimeType> builder)
        {
             builder.ToTable("TaxRegimeType").HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("TaxRegimeTypeId");

            builder.Property(s => s.Name)
               .HasMaxLength(60)
               .HasConversion(s => s.Value, s => new Name(s))
               .IsRequired();


            builder.Property(s => s.Description)
               .HasMaxLength(60)
               .HasConversion(s => s.Value, s => new GeneralText(s))
               .IsRequired();
        }
    }
}
