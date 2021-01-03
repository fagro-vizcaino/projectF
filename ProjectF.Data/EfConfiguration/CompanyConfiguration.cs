using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company").HasKey(c => c.CompanyId);
            builder.Property(c => c.CompanyId).HasColumnName("CompanyId");

            builder.Property(q => q.Name)
                .HasMaxLength(60)
                .HasConversion(p => p.Value, p => new Name(p))
                .IsRequired();

              builder.Property(q => q.Rnc)
                .HasMaxLength(18);

            builder.Property(q => q.HomeOrApartment)
                .HasMaxLength(50);

            builder.Property(q => q.City)
                .HasMaxLength(30);

            builder.Property(q => q.Street)
                .HasMaxLength(220);

            builder.HasOne(p => p.Country).WithMany();

              builder.Property(q => q.Phone)
                .HasMaxLength(11)
                .HasConversion(p => p.Value, p => new Phone(p));

            builder.Property(c => c.Status)
                .IsRequired();
            
            builder.Property(c => c.Created)
                .IsRequired()
                .HasColumnType("Datetime");
            
            builder.Property(c => c.Modified)
                .HasColumnType("Datetime");

        }
    }
}
