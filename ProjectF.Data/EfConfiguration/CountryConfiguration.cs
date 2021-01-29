using ProjectF.Data.Entities.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ProjectF.Data.EfConfiguration
{
    class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("CountryId");

            builder.Property(p => p.Name)
              .HasMaxLength(120)
              .IsRequired();

            builder.Property(p => p.IconImage)
              .HasMaxLength(800);
        }

        public static IEnumerable<Country> InitialCountryData()
        {
            yield return new Country(1, "Dominican Republic", string.Empty);
            yield return new Country(2, "Puerto Rico", string.Empty);
            yield return new Country(3, "Panama", string.Empty);
            yield return new Country(4, "Colombia", string.Empty);
        }
    }
}