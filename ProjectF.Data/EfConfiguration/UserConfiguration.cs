using ProjectF.Data.Entities.Auth;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        readonly long _companyId;

        public UserConfiguration(){ }
        public UserConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }
       public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(u => u.Id);
            
            builder.Property(u => u.Id).HasColumnName("UserId");
            
            builder.Property(u => u.Firstname)
            .HasMaxLength(65)
            .IsRequired();

            builder.Property(u => u.Lastname)
            .HasMaxLength(220);

            builder.HasOne(u => u.Country).WithMany()
                .IsRequired(false);

            builder.HasOne<Company>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.HasQueryFilter(x => x.CompanyId == _companyId);
        }
    }
}
