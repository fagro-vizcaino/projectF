using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        readonly long _companyId;
        public CategoryConfiguration() { }
        public CategoryConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("CategoryId");

            builder.Property(c => c.Code)
                .HasMaxLength(20)
                .HasConversion(c => c.Value, c => new Code(c))
                .IsRequired();

            builder.Property(q => q.Name)
                .HasMaxLength(60)
                .HasConversion(p => p.Value, p => new Name(p))
                .IsRequired();

            builder.Property(q => q.ShowOn)
                .HasColumnType("bit")
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
