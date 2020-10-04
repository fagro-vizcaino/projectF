using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    class PaymentTermConfiguration : IEntityTypeConfiguration<PaymentTerm>
    {
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
        }
    }
}
