using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    class InvoiceHeaderConfiguration : IEntityTypeConfiguration<InvoiceHeader>
    {
        readonly long _companyId;

        public InvoiceHeaderConfiguration() { }
        public InvoiceHeaderConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }
        public void Configure(EntityTypeBuilder<InvoiceHeader> builder)
        {
            builder.ToTable("InvoiceHeader").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("InvoiceHeaderId");

            builder.Property(c => c.Code)
                .HasMaxLength(20)
                .HasConversion(c => c.Value, c => new Code(c))
                .IsRequired();

            builder.Property(q => q.NumberSequenceId)
                .IsRequired();

            builder.Property(q => q.Ncf)
                .HasMaxLength(20);

            builder.Property(q => q.Rnc)
               .HasMaxLength(15);

            builder.HasOne(q => q.Client);
           

            builder.Property(q => q.InvoiceDate)
                .IsRequired();

            builder.Property(q => q.DueDate)
                .IsRequired();

            builder.HasOne(q => q.PaymentTerm);

            builder.Property(q => q.Notes)
                .HasMaxLength(220)
                .HasConversion(p => p.Value, p => new GeneralText(p));

            builder.Property(q => q.Discount)
             .HasColumnType("decimal(12,2)");

            builder.Property(q => q.SubTotal)
             .HasColumnType("decimal(12,2)");

            builder.Property(q => q.TaxTotal)
             .HasColumnType("decimal(12,2)");

            builder.Property(q => q.Total)
             .HasColumnType("decimal(12,2)");

            builder.Property(q => q.TermAndConditions)
                .HasMaxLength(220)
                .HasConversion(p => p.Value, p => new GeneralText(p));

            builder.Property(q => q.Footer)
                .HasMaxLength(220)
                .HasConversion(p => p.Value, p => new GeneralText(p));

            builder.HasMany(q => q.InvoiceDetails).WithOne(q => q.InvoiceHeader);

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
