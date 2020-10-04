using ProjectF.Data.Entities.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ProjectF.Data.EfConfiguration
{
    class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currency").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("CurrencyId");

            builder.Property(p => p.Name)
              .HasMaxLength(120)
              .IsRequired();
        }

        public static List<Currency> InitialCountryData()
        {
            var currencies = new List<Currency>
      {
        new Currency(1,"DOP Peso Dominicano")
        , new Currency(2, "US Dolar USA")
      };

            return currencies;
        }
    }
}