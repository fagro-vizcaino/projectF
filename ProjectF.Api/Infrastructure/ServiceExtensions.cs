using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using ProjectF.Application.Auth;
using ProjectF.Application.Banks;
using ProjectF.Application.Categories;
using ProjectF.Application.Clients;
using ProjectF.Application.Countries;
using ProjectF.Application.Invoice;
using ProjectF.Application.NumberSequences;
using ProjectF.Application.PaymentMethods;
using ProjectF.Application.PaymentTerms;
using ProjectF.Application.Products;
using ProjectF.Application.Suppliers;
using ProjectF.Application.Taxes;
using ProjectF.Application.Warehouses;
using ProjectF.Data.Context;
using ProjectF.Data.Entities.Auth;
using ProjectF.Data.Repositories;
using ProjectF.Application.Companies;
using ProjectF.Application.UnitOfMeasures;

namespace ProjectF.Api.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit                 = false;
                o.Password.RequireLowercase             = false;
                o.Password.RequireUppercase             = false;
                o.Password.RequireNonAlphanumeric       = false;
                o.Password.RequiredLength               = 10;
                o.User.RequireUniqueEmail               = true;
                o.SignIn.RequireConfirmedEmail          = true;
                o.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<_AppDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation"); ;

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
               opt.TokenLifespan = TimeSpan.FromHours(2));

            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(1));
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET") ?? string.Empty;

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<CategoryCrudHandler>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<WarehouseCrudHandler>();
            services.AddScoped<WerehouseRepository>();
            services.AddScoped<AuthUserCrudHandler>();
            services.AddScoped<UserRepository>();
            services.AddScoped<CountryRepository>();
            services.AddScoped<CountryCrudOperation>();
            services.AddScoped<ProductCrudHandler>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<InvoiceCrudHandler>();
            services.AddScoped<InvoiceMainListHandler>();
            services.AddScoped<InvoiceRepository>();
            services.AddScoped<ClientCrudHandler>();
            services.AddScoped<ClientRepository>();
            services.AddScoped<SupplierRepository>();
            services.AddScoped<SupplierCrudHandler>();
            services.AddScoped<TaxRepository>();
            services.AddScoped<TaxCrudHandler>();
            services.AddScoped<PaymentTermRepository>();
            services.AddScoped<PaymentTermCrudHandler>();
            services.AddScoped<BankAccountTypeRepository>();
            services.AddScoped<BankAccountRepository>();
            services.AddScoped<BankAccountCrudHandler>();
            services.AddScoped<BankAccountTypeCrudHandler>();
            services.AddScoped<DocumentNumberSequenceRepository>();
            services.AddScoped<DocumentNumberSequenceHandler>();
            services.AddScoped<PaymentMethodHandler>();
            services.AddScoped<PaymentMethodRepository>();

            services.AddScoped<UnitOfMeasureRepository>();
            services.AddScoped<UnitOfMeasureCrudHandler>();

            services.AddScoped<CompanyCrudHandler>();
            services.AddScoped<CompanyRepository>();

        }

        public static void ConfigureMvcViews(this IServiceCollection services)
        {
            services.AddMvc(o => o.Conventions.Add(new FeatureConvention()))
           .AddRazorOptions(options =>
           {
               // Replace normal view location entirely
               // {0} - Action Name
               // {1} - Controller Name
               // {2} - Area Name
               // {3} - Feature Name
               options.AreaViewLocationFormats.Clear();
               options.AreaViewLocationFormats.Add("/Areas/{2}/Features/{3}/{1}/{0}.cshtml");
               options.AreaViewLocationFormats.Add("/Areas/{2}/Features/{3}/{0}.cshtml");
               options.AreaViewLocationFormats.Add("/Areas/{2}/Features/Shared/{0}.cshtml");
               options.AreaViewLocationFormats.Add("/Areas/Shared/{0}.cshtml");

               // replace normal view location entirely
               options.ViewLocationFormats.Clear();
               options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
               options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
               options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
               options.ViewLocationExpanders.Add(new FeatureFoldersRazorViewEngine());
           });
        }

        public static void ConfigureAppDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<_AppDbContext>(options =>
               options
               .UseLoggerFactory(_AppDbContext.GetLoggerFactory())
               .EnableSensitiveDataLogging()
               .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
