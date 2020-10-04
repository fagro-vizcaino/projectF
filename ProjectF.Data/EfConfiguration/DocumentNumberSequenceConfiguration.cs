using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Sequences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    class DocumentNumberSequenceConfiguration : IEntityTypeConfiguration<DocumentNumberSequence>
    {
        public void Configure(EntityTypeBuilder<DocumentNumberSequence> builder)
        {
             builder.ToTable("DocumentNumberSequence").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("DocumentNumberSequenceId");

            builder.Property(q => q.DocumentType)
                .IsRequired();

            builder.Property(p => p.Name)
               .HasMaxLength(60)
               .HasConversion(p => p.Value, p => new Name(p))
               .IsRequired();

            builder.Property(p => p.Prefix)
               .HasMaxLength(20)
               .HasConversion(p => p.Value, p => new Code(p))
               .IsRequired();


            builder.Property(p => p.Prefix);

             builder.Property(p => p.Sequence)
               .HasMaxLength(60)
               .HasConversion(p => p.Value, p => new NumberSequence(p));

            builder.Property(q => q.FinalSequence);

            builder.Property(q => q.ValidFrom);
            builder.Property(q => q.ValidTo);
            builder.Property(q => q.IsDefault)
                .HasDefaultValue(false)
                .IsRequired();

        }
    }
}
