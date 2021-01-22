using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.UnitOfMeasures;

namespace ProjectF.Data.EfConfiguration
{
    class UnitOfMeasureConfiguration : IEntityTypeConfiguration<UnitOfMeasure>
    {
        readonly long _companyId;
        public UnitOfMeasureConfiguration() { }
        public UnitOfMeasureConfiguration(long companyId): this()
            => _companyId = companyId;

        public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            builder.ToTable("UnitOfMeasure").HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("UnitOfMeasureId");

            builder.Property(u => u.Name)
                .HasMaxLength(60)
                .HasConversion(u => u.Value, u => new Name(u))
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
