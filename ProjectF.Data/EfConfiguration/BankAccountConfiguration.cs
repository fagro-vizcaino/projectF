using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectF.Data.Entities;

namespace ProjectF.Data.EfConfiguration
{
    class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccount").HasKey(w => w.Id);
            builder.Property(w => w.Id).HasColumnName("BankAccountId");
            
             builder.Property(p => p.AccountName)
                .HasMaxLength(60)
                .HasConversion(p => p.Value, p => new Name(p))
                .IsRequired();

             builder.Property(q => q.AccountNumber)
                .HasMaxLength(18);

            builder.Property(q => q.Description)
                .HasMaxLength(220)
                .HasConversion(p => p.Value, p => new GeneralText(p));

            builder.HasOne(q => q.BankAccountType);
            
            builder.Property(q => q.InitialBalance)
                .HasColumnType("decimal(16,2)")
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

            
        }
    }
}
