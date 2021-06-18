using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.GoodsTypes;

namespace ProjectF.Data.EfConfiguration
{
    public class GoodsTypeConfiguration : IEntityTypeConfiguration<GoodsType>
    {
        public void Configure(EntityTypeBuilder<GoodsType> builder)
        {
            builder.ToTable("GoodsType").HasKey(c => c.Id)
                .IsClustered();

            builder.Property(c => c.Id)
                .HasColumnName("GoodsTypeId")
                .ValueGeneratedOnAdd();


            builder.Property(c => c.Description)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(c => c.Code)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired();
        }

        public static IEnumerable<GoodsType> InitialGoodsTypeData()
        {
              yield return new(1, "1","Gastos de personal", EntityStatus.Active);
              yield return new(2, "2","Gastos por trabajos, Suministros y servicios", EntityStatus.Active);
              yield return new(3, "3","Arrendamiento", EntityStatus.Active);
              yield return new(4, "4","Gastos de activo fijo", EntityStatus.Active);
              yield return new(5, "5","Gastos de representación", EntityStatus.Active);
              yield return new(6, "6","Otras deduciones", EntityStatus.Active);
              yield return new(7, "7","Gastos Financieros", EntityStatus.Active);
              yield return new(8, "8","Gastos Extraordinarios", EntityStatus.Active);
              yield return new(9, "9","Compras y gastos que formaran parde del costo de venta", EntityStatus.Active);
              yield return new(10, "10","Adquisiciones de activos", EntityStatus.Active);
              yield return new(11, "11","Gastos de seguros", EntityStatus.Active);
        }
    }
}
