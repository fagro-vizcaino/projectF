using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectF.Api.Infrastructure;
using ProjectF.Data.Context;
using Microsoft.EntityFrameworkCore;
using ProjectF.Application.Categories;
using ProjectF.Data.Repositories;
using ProjectF.Application.Auth;
using ProjectF.Application.Countries;
using ProjectF.Application.Werehouses;
using ProjectF.Application.Products;
using ProjectF.Application.Invoice;
using ProjectF.Application.Clients;
using ProjectF.Application.Suppliers;
using ProjectF.Application.PaymentTerms;
using ProjectF.Application.Banks;
using ProjectF.Application.Taxes;
using ProjectF.Application.NumberSequence;
using ProjectF.Application.PaymentMethods;
using ProjectF.EmailService;
using Microsoft.AspNetCore.Routing;
using ProjectF.EmailService.Auth;
using System;

namespace ProjectF.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<CategoryCrudHandler>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<WerehouseCrudHandler>();
            services.AddScoped<WerehouseRepository>();

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

            //Db Related stuffs.
            services.AddDbContext<_AppDbContext>(options =>
                options
                .UseLoggerFactory(_AppDbContext.GetLoggerFactory())
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

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

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            emailConfig.UserName = Environment.GetEnvironmentVariable("emailusername") ?? string.Empty;
            emailConfig.Password = Environment.GetEnvironmentVariable("emailpassword") ?? string.Empty;

            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            var authHtmlTemplate = Configuration
                .GetSection("AuthTemplates")
                .Get<AuthHtmlTemplateConfig>();

            services.AddSingleton(authHtmlTemplate);

            services.AddScoped<AuthUserCrudHandler>();
            services.AddControllers();
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                
                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                .RequireAuthorization();
            });
        }
    }
}
