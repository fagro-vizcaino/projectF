using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProjectF.WebUI.Services;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Pages.Products;
using ProjectF.WebUI.Pages;
using FluentValidation;
using ProjectF.WebUI.Components.Common;

namespace ProjectF.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string baseUrl = "http://localhost:5000/api/";
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddAntDesign();

            builder.Services.AddHttpClient<IBaseDataService<Category>, CategoryDataService>(client
                => client.BaseAddress = new Uri(baseUrl));
            builder.Services.AddHttpClient<IBaseDataService<Werehouse>, WerehouseDataService>(client
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


            builder.Services.AddTransient<IValidator<Category>, CategoryValidator>();
            builder.Services.AddTransient<IValidator<Werehouse>, WerehouseValidator>();
            builder.Services.AddTransient<IValidator<Tax>, TaxValidator>();
            builder.Services.AddTransient<IValidator<PaymentTerm>, PaymentTermValidator>();
            builder.Services.AddTransient<IValidator<Bank>, BankValidator>();
            builder.Services.AddTransient<IValidator<BankAccountType>, BankAccountTypeValidator>();
            builder.Services.AddTransient<IValidator<Client>, ClientValidator>();
            builder.Services.AddTransient<IValidator<Supplier>, SupplierValidator>();
            builder.Services.AddTransient<IValidator<Product>, ProductValidator>();


            builder.Services.AddTransient<IFMessage, FMessage>();

            await builder.Build().RunAsync();
        }
    }
}
