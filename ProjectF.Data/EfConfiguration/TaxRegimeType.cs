using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    public class TaxRegimeTypeConfiguration : IEntityTypeConfiguration<TaxRegimeType>
    {
        readonly long _companyId;

        public TaxRegimeTypeConfiguration(){}
        public TaxRegimeTypeConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }
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
