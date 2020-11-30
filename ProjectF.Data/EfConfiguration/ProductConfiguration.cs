using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;

namespace ProjectF.Data.EfConfiguration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("ProductId");

            builder.Property(c => c.Code)
                .HasMaxLength(20)
                .HasConversion(c => c.Value, c => new Code(c))
                .IsRequired();

            builder.Property(q => q.Name)
                .HasMaxLength(60)
                .HasConversion(p => p.Value, p => new Name(p))
                .IsRequired();

            builder.Property(q => q.Description)
                .HasMaxLength(60)
                .HasConversion(p => p.Value, p => new GeneralText(p));

              builder.Property(q => q.Reference)
                .HasMaxLength(60)
                .HasConversion(p => p.Value, p => new GeneralText(p));
            
            builder.HasOne(p => p.Category)
                .WithMany()
                .IsRequired();

            builder.HasOne(p => p.Warehouse);

            builder.HasOne(p => p.Tax);

            builder.Property(q => q.IsService)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(q => q.Cost)
                .HasColumnType("decimal(16,2)");

            builder.Property(q => q.Price)
                .HasColumnType("decimal(16,2)");

            builder.Property(q => q.Price2)
                .HasColumnType("decimal(16,2)");

            builder.Property(q => q.Price3)
                .HasColumnType("decimal(16,2)");

            builder.Property(q => q.Price4)
                .HasColumnType("decimal(16,2)");

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
