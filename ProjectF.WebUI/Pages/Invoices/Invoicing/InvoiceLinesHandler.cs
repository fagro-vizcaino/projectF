using AntDesign;
using LanguageExt;
using Microsoft.AspNetCore.Components;
using static LanguageExt.Prelude;
using ProjectF.WebUI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneOf;
using Microsoft.JSInterop;
using ProjectF.WebUI.Pages.Products;

namespace ProjectF.WebUI.Pages.Invoices.Invoicing
{
    public class InvoiceLinesHandler : ComponentBase
    {
        [Parameter]
        public Product[] ProductDataSource { get; set; }
        [Parameter]
        public EventCallback<List<InvoiceLine>> OnLineChanged { get; set; }
        [Parameter]
        public List<InvoiceLine> InvoiceLines { get; set; }
        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        protected override Task OnInitializedAsync()
        {
            Console.WriteLine("On initialized async invoice lines");
            ProductDataSource = ProductDataSource.Length > 0 
                ? ProductDataSource
                : System.Array.Empty<Product>();
            
            InvoiceLines = (InvoiceLines?.Count(l => l.Id > 0)) >= 1 
                ? InvoiceLines
                : new List<InvoiceLine> { GetEmptyLine() };

            return Task.CompletedTask;
        }

        protected override Task OnParametersSetAsync()
        {
           // IsEdit(InvoiceLines);
            Console.WriteLine("On parameter set async invoice lines");
            Console.WriteLine($"Product {ProductDataSource.Length}");
            Console.WriteLine($"Line {InvoiceLines.Count}");
            return base.OnParametersSetAsync();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine($"producs {ProductDataSource.Length}");
            return base.OnAfterRenderAsync(firstRender);
        }

        protected Product GetProduct(string searchText)
            => ProductDataSource.FirstOrDefault(p => p.Id == parseInt(searchText).Match(r => r, () => 0));

        protected readonly Func<string, SelectOption, bool> FilterOptionValue = FilterOption;

        protected static bool FilterOption(string value, SelectOption option)
            => option.Children.ToUpperInvariant().Contains(value.ToLower(), StringComparison.OrdinalIgnoreCase);

        protected void OnChange(OneOf<string, IEnumerable<string>, LabeledValue, IEnumerable<LabeledValue>> value,
            OneOf<SelectOption, IEnumerable<SelectOption>> option, InvoiceLine line)
        {
            var product = GetProduct(value.Value.ToString());
            line.Product = product;
            line.Qty     = 1;
            line.IsEmpty = false;
            InvoiceLines.Add(GetEmptyLine());
            OnLineChanged.InvokeAsync(InvoiceLines.ToList());
        }

        public void ClearLines()
        {
            InvoiceLines.RemoveAll(l => l.Index >= 0);
            InvoiceLines.ForEach( l => l.CurrentSelect.ClearAll());
            UpdateEmptyLines();
            ((IJSInProcessRuntime)jSRuntime).InvokeVoid("removeLine");
            OnLineChanged.InvokeAsync(InvoiceLines.ToList());
        }

        protected void Remove(InvoiceLine line)
        {
            if (line.IsEmpty) return;
            line.IsDelete = true;
            line.CurrentSelect.ClearAll();
            ((IJSInProcessRuntime)jSRuntime).InvokeVoid("removeLine");   
            OnLineChanged.InvokeAsync(InvoiceLines.ToList());
        }

        public InvoiceLine GetEmptyLine()
            => new InvoiceLine() { 
                Product = new Product() { Tax = new Tax() }
                , IsEmpty = true
                , IsDelete = false
                , Index = (InvoiceLines?.Count ?? 0) + 1 };

        void UpdateEmptyLines()
        {
            var shouldAddLines = InvoiceLines.Count - InvoiceLines.Count(i => i.Product?.Id != 0) == 1;
            if (shouldAddLines) return;

            var line = GetEmptyLine();
            InvoiceLines.Add(line);
        }

    }

}
