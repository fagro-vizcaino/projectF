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

namespace ProjectF.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var http = new HttpClient()
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };
            builder.Services.AddScoped(sp => http);

            using var response = await http.GetAsync("appsettings.json");
            using var stream = await response.Content.ReadAsStreamAsync();

            builder.Configuration.AddJsonStream(stream);
            var baseUrl = builder.Configuration.GetValue<string>("httpService");
            

            //Auth
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);

            //builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


            //Themes
            builder.Services.AddAntDesign();

            builder.Services.AddHttpClient<IBaseDataService<Category>, CategoryDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<Warehouse>, WarehouseDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<Tax>, TaxesDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<PaymentTerm>, PaymentTermDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<Bank>, BankDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<BankAccountType>, BankAccountTypeDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<Country>, CountryDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<Client>, ClientDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<Supplier>, SupplierDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<Product>, ProductDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<Invoice>, InvoiceDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<InvoiceMainList>, InvoiceListDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<NumberSequence>, NumberSequenceDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(c 
                => c.BaseAddress = new Uri(baseUrl));

            builder.Services.AddTransient<IValidator<Category>, CategoryValidator>();
            builder.Services.AddTransient<IValidator<Warehouse>, WarehouseValidator>();
            builder.Services.AddTransient<IValidator<Tax>, TaxValidator>();
            builder.Services.AddTransient<IValidator<PaymentTerm>, PaymentTermValidator>();
            builder.Services.AddTransient<IValidator<Bank>, BankValidator>();
            builder.Services.AddTransient<IValidator<BankAccountType>, BankAccountTypeValidator>();
            builder.Services.AddTransient<IValidator<Client>, ClientValidator>();
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
