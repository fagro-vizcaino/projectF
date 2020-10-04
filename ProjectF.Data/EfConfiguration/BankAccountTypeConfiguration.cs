using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.EfConfiguration
{
    class BankAccountTypeConfiguration : IEntityTypeConfiguration<BankAccountType>
    {
        public void Configure(EntityTypeBuilder<BankAccountType> builder)
        {
            builder.ToTable("BankAccountType").HasKey(w => w.Id);
            builder.Property(w => w.Id).HasColumnName("BankAccountTypeId");
            
             builder.Property(p => p.Name)
                .HasMaxLength(60)
                .HasConversion(p => p.Value, p => new Name(p))
                .IsRequired();

             builder.Property(q => q.Description)
                .HasMaxLength(119)
                .HasConversion(p => p.Value, p => new GeneralText(p));

        }
    }
}
