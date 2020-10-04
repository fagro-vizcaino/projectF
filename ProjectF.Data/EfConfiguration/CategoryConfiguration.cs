using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectF.Data.EfConfiguration
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
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

        }
    }
}
