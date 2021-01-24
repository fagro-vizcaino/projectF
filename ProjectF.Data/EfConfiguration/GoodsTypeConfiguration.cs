﻿using System.Collections.Generic;
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
            builder.ToTable("GoodsType").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("GoodsTypeId");


            builder.Property(c => c.Description)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(c => c.Code)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired();
        }

        public static List<GoodsType> InitialGoodsTypeData()
        {
            var goodsTypes = new List<GoodsType>
            {
               new(0, "1","Gastos de personal", EntityStatus.Active)
               , new(0, "2","Gastos por trabajos, Suministros y servicios", EntityStatus.Active)
               , new(0, "3","Arrendamiento", EntityStatus.Active)
               , new(0, "4","Gastos de activo fijo", EntityStatus.Active)
               , new(0, "5","Gastos de representación", EntityStatus.Active)
               , new(0, "6","Otras deduciones", EntityStatus.Active)
               , new(0, "7","Gastos Financieros", EntityStatus.Active)
               , new(0, "8","Gastos Extraordinarios", EntityStatus.Active)
               , new(0, "9","Compras y gastos que formaran parde del costo de venta", EntityStatus.Active)
               , new(0, "10","Adquisiciones de activos", EntityStatus.Active)
               , new(0, "11","Gastos de seguros", EntityStatus.Active)
            };

            return goodsTypes;
        }
    }
}
