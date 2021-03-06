﻿using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.EfConfiguration
{
    class BankAccountTypeConfiguration : IEntityTypeConfiguration<BankAccountType>
    {
        readonly long _companyId;

        public BankAccountTypeConfiguration() { }
        public BankAccountTypeConfiguration(long companyId) : this()
        {
            _companyId = companyId;
        }

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
