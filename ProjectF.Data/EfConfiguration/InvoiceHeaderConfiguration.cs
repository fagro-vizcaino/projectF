using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    class InvoiceHeaderConfiguration : IEntityTypeConfiguration<InvoiceHeader>
    {
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
            builder.HasOne(q => q.Company);

            builder.Property(q => q.Created)
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

            builder.Property(q => q.SystemCreated);
            builder.Property(q => q.SystemModified);

        }
    }
}
