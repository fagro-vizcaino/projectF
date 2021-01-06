using ProjectF.Data.Entities.Clients;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LanguageExt;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    class UserClientConfiguration : IEntityTypeConfiguration<Client>
    {
        readonly long _companyId;

        public UserClientConfiguration(){}
        public UserClientConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client").HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("ClientId");

            builder.Property(s => s.Code)
               .HasMaxLength(20)
               .HasConversion(s => s.Value, s => new Code(s))
               .IsRequired();

            builder.Property(s => s.Firstname)
               .HasMaxLength(60)
               .HasConversion(s => s.Value, s => new Name(s))
               .IsRequired();

            builder.Property(s => s.Lastname)
               .HasMaxLength(120)
               .HasConversion(s => s.Value, s => new Name(s));

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

            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasQueryFilter(x => x.CompanyId == _companyId
          && x.Status == EntityStatus.Active);

        }
    }
}
