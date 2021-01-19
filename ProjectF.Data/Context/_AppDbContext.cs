using ProjectF.Data.EfConfiguration;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Auth;
using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Clients;
using ProjectF.Data.Entities.Countries;
using ProjectF.Data.Entities.Currencies;
using ProjectF.Data.Entities.Invoices;
using ProjectF.Data.Entities.PaymentList;
using ProjectF.Data.Entities.Sequences;
using ProjectF.Data.Entities.Suppliers;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType;
using ProjectF.Data.Entities.Warehouses;
using ProjectF.Data.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using ProjectF.Data.Entities.UnitOfMeasures;
using ProjectF.Data.Entities.PaymentMethods;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.Context
{
    public class _AppDbContext : IdentityDbContext<User>
    {
        readonly long _companyId;
        readonly string _userId;
        public DbSet<Category> Categories { get; set; }
        public DbSet<Warehouse> Werehouses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<DocumentNumberSequence> DocumentNumberSequences { get; set; }
        public DbSet<TaxRegimeType> TaxRegimeTypes { get; set; }
        public DbSet<PaymentTerm> PaymentMethods { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }

        public _AppDbContext(DbContextOptions<_AppDbContext> options
            , IGetClaimsProvider userData) 
            : base(options)
        {
            _companyId = parseInt(userData.CompanyId).Match(c => c, () => 0);
            _userId = userData.UserId;
        }

        public override int SaveChanges()
        {
            this.MarkCreatedItemAsOwnedBy(_companyId);
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.MarkCreatedItemAsOwnedBy(_companyId);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.MarkCreatedItemAsOwnedBy(_companyId);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.MarkCreatedItemAsOwnedBy(_companyId);
            return base.SaveChangesAsync(cancellationToken);
        }

        public static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.Entity<Category>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.Entity<Warehouse>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.Entity<Supplier>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
            modelBuilder.Entity<BankAccount>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new BankAccountTypeConfiguration());
            modelBuilder.Entity<BankAccountType>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);
           
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());

            modelBuilder.ApplyConfiguration(new UserClientConfiguration());
            modelBuilder.Entity<Client>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new PaymentTermConfiguration());
            modelBuilder.Entity<PaymentTerm>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.Entity<Product>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new TaxConfiguration());
            modelBuilder.Entity<Tax>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new UnitOfMeasureConfiguration());
                modelBuilder.Entity<UnitOfMeasure>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);
            
            modelBuilder.ApplyConfiguration(new TaxRegimeTypeConfiguration());
                modelBuilder.Entity<TaxRegimeType>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new DocumentNumberSequenceConfiguration());
            modelBuilder.Entity<DocumentNumberSequence>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new InvoiceHeaderConfiguration());
            modelBuilder.Entity<InvoiceHeader>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
            modelBuilder.Entity<InvoiceDetail>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);

            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
            modelBuilder.Entity<PaymentMethod>()
                .HasQueryFilter(c => c.CompanyId == _companyId && c.Status == EntityStatus.Active);
            
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.Entity<Country>().HasData(CountryConfiguration.InitialCountryData());

            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.Entity<Currency>().HasData(CurrencyConfiguration.InitialCurrencyData());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
