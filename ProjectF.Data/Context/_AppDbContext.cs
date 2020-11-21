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
using ProjectF.Data.Entities.PaymentMethods;

namespace ProjectF.Data.Context
{
    public class _AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
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
        public _AppDbContext(DbContextOptions<_AppDbContext> options) : base(options) { }

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

            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
            modelBuilder.ApplyConfiguration(new BankAccountTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new UserClientConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTermConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new TaxConfiguration());
            modelBuilder.ApplyConfiguration(new TaxRegimeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentNumberSequenceConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceHeaderConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.Entity<Country>().HasData(CountryConfiguration.InitialCountryData());

            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.Entity<Currency>().HasData(CurrencyConfiguration.InitialCurrencyData());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
