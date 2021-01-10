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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using System.Threading;

namespace ProjectF.Data.Context
{
    public class _AppDbContext : IdentityDbContext<User>
    {
        readonly long _companyId;
        readonly string _userId;
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
        public _AppDbContext(DbContextOptions<_AppDbContext> options, IGetClaimsProvider userData) 
            : base(options)
        {
            _companyId = 0;
            if (long.TryParse(userData.CompanyId, out var companyId))
             {
                _companyId = companyId;
             }
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

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new SupplierConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new BankAccountConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new BankAccountTypeConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new UserClientConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new PaymentTermConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new ProductConfiguration(_companyId));
            
            modelBuilder.ApplyConfiguration(new TaxConfiguration(_companyId));
            
            
            modelBuilder.ApplyConfiguration(new TaxRegimeTypeConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new DocumentNumberSequenceConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new InvoiceHeaderConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new InvoiceDetailConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration(_companyId));
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.Entity<Country>().HasData(CountryConfiguration.InitialCountryData());

            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.Entity<Currency>().HasData(CurrencyConfiguration.InitialCurrencyData());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
