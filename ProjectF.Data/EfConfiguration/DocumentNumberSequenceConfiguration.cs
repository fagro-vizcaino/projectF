using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Sequences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;

namespace ProjectF.Data.EfConfiguration
{
    class DocumentNumberSequenceConfiguration : IEntityTypeConfiguration<DocumentNumberSequence>
    {
        public void Configure(EntityTypeBuilder<DocumentNumberSequence> builder)
        {
             builder.ToTable("DocumentNumberSequence").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("DocumentNumberSequenceId");

            builder.Property(p => p.Name)
               .HasMaxLength(60)
               .HasConversion(p => p.Value, p => new Name(p))
               .IsRequired();

            builder.Property(p => p.Prefix)
               .HasMaxLength(20)
               .IsRequired();

            builder.Property(p => p.InitialSequence)
                .IsRequired();

             builder.Property(p => p.NextSequence)
               .IsRequired();

            builder.Property(q => q.FinalSequence)
                .IsRequired();

            builder.Property(q => q.ValidUntil);

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
