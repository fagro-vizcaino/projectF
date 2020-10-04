using ProjectF.Data.Entities.Clients;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LanguageExt;

namespace ProjectF.Data.EfConfiguration
{
    class UserClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client").HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("ClientId");

            builder.Property(s => s.Code)
               .HasMaxLength(20)
               .HasConversion(s => s.Value, s => new Code(s))
               .IsRequired();

            builder.Property(s => s.Name)
               .HasMaxLength(60)
               .HasConversion(s => s.Value, s => new Name(s))
               .IsRequired();

            builder.Property(s => s.Email)
                .HasMaxLength(60)
                .HasConversion(s => s.Value, s => new Email(s));

            builder.Property(s => s.Phone)
                .HasMaxLength(11)
                .HasConversion(s => s.Value, s => new Phone(s));

            builder.Property(s => s.Rnc)
                .HasMaxLength(15);

            builder.Property(s => s.Birthday)
                .HasColumnType("Date");

            builder.HasOne(p => p.Country).WithMany();

            builder.Property(s => s.HomeOrApartment)
                .HasMaxLength(200);

            builder.Property(s => s.City)
                .HasMaxLength(60);

            builder.Property(s => s.Street)
                .HasMaxLength(60);

        }
    }
}
