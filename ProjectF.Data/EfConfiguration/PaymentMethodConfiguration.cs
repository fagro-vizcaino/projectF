using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentMethods;

namespace ProjectF.Data.EfConfiguration
{
    class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        readonly long _companyId;

        public PaymentMethodConfiguration(){ }
        public PaymentMethodConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethod").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("PaymentMethodId");
            builder.Property(c => c.Status);

            builder.Property(c => c.Code)
                .HasMaxLength(20)
                .HasConversion(c => c.Value, c => new Code(c))
                .IsRequired();

            builder.Property(q => q.Description)
                .HasMaxLength(60)
                .HasConversion(p => p.Value, p => new Name(p))
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
