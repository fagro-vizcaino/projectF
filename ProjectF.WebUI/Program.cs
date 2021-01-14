using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProjectF.WebUI.Services;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Pages.Products;
using ProjectF.WebUI.Pages;
using ProjectF.WebUI.Pages.Invoices;
using FluentValidation;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Pages.Invoices.List;
using ProjectF.WebUI.Pages.NumberSequences;
using ProjectF.WebUI.AuthProviders;
using Microsoft.AspNetCore.Components.Authorization;
using ProjectF.WebUI.Pages.Auth;
using Blazored.LocalStorage;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using ProjectF.WebUI.Pages.Company;
using ProjectF.WebUI.Pages.UnitOfMeasures;

namespace ProjectF.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            HttpClient appSettingConfig = new() { BaseAddress = new(builder.HostEnvironment.BaseAddress) };
            
            using var response = await appSettingConfig.GetAsync($"appsettings.{builder.HostEnvironment.Environment}.json");
            using var stream = await response.Content.ReadAsStreamAsync();

            builder.Configuration.AddJsonStream(stream);
            var baseUrl = builder.Configuration.GetValue<string>("httpService");
            
            //Base HttpClient
            HttpClient httpClient = new() { BaseAddress = new(baseUrl) };
            builder.Services.AddScoped(sp => httpClient);

            //Auth
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            //Themes
            builder.Services.AddAntDesign();

            //Services
            builder.Services.AddScoped<IBaseDataService<Tax>, TaxesDataService>();
            builder.Services.AddScoped<IBaseDataService<PaymentTerm>, PaymentTermDataService>();
            builder.Services.AddScoped<IBaseDataService<Bank>, BankDataService>();
            builder.Services.AddScoped<IBaseDataService<BankAccountType>, BankAccountTypeDataService>();
            builder.Services.AddScoped<IBaseDataService<Country>, CountryDataService>();

            builder.Services.AddScoped<IBaseDataService<Client>, ClientDataService>();
            builder.Services.AddScoped<IBaseDataService<Supplier>, SupplierDataService>();
            builder.Services.AddScoped<IBaseDataService<Company>, CompanyDataService>();

            builder.Services.AddScoped<IBaseDataService<Product>, ProductDataService>();
            builder.Services.AddScoped<IBaseDataService<Category>, CategoryDataService>();
            builder.Services.AddScoped<IBaseDataService<Warehouse>, WarehouseDataService>();

            builder.Services.AddScoped<IBaseDataService<Invoice>, InvoiceDataService>();
            builder.Services.AddScoped<IBaseDataService<InvoiceMainList>, InvoiceListDataService>();
            builder.Services.AddScoped<IBaseDataService<NumberSequence>, NumberSequenceDataService>();
            
            //Validators
            builder.Services.AddTransient<IValidator<Category>, CategoryValidator>();
            builder.Services.AddTransient<IValidator<Warehouse>, WarehouseValidator>();
            builder.Services.AddTransient<IValidator<Tax>, TaxValidator>();
            builder.Services.AddTransient<IValidator<PaymentTerm>, PaymentTermValidator>();
            builder.Services.AddTransient<IValidator<Bank>, BankValidator>();
            builder.Services.AddTransient<IValidator<BankAccountType>, BankAccountTypeValidator>();
            builder.Services.AddTransient<IValidator<Client>, ClientValidator>();
            builder.Services.AddTransient<IValidator<Company>, CompanyValidator>();
            builder.Services.AddTransient<IValidator<UnitOfMeasure>, UnitOfMeasureValidator>();
            builder.Services.AddTransient<IValidator<Supplier>, SupplierValidator>();
            builder.Services.AddTransient<IValidator<Product>, ProductValidator>();
            builder.Services.AddTransient<IValidator<NumberSequence>, NumberSequenceValidator>();
            builder.Services.AddTransient<IValidator<UserRegisterDto>, UserRegisterValidator>();
            builder.Services.AddTransient<IValidator<UserLoginDto>, UserLoginValidator>();
            builder.Services.AddTransient<IValidator<UserForgotPasswordDto>, UserForgotPasswordValidator>();
            builder.Services.AddTransient<IValidator<UserResetPasswordDto>, UserResetPasswordValidator>();

            builder.Services.AddSingleton<AlertService>();

            //themes 
            builder.Services.AddTransient<IFMessage, FMessage>();

            await builder.Build().RunAsync();
        }
    }
}
