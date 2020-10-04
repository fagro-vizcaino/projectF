using ProjectF.Data.Entities.Auth;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
       public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(u => u.Id);
            
            builder.Property(u => u.Id).HasColumnName("UserId");
            
            builder.Property(u => u.Fullname)
            .HasMaxLength(200)
            .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(30)
                .HasConversion(u => u.Value, u => new Email(u));

            builder.HasOne(u => u.Country).WithMany();
      }
    }
}
