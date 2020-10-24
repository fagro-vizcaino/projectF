using AntDesign;
using LanguageExt;
using Microsoft.AspNetCore.Components;
using static LanguageExt.Prelude;
using static System.Text.Json.JsonSerializer;
using ProjectF.WebUI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneOf;
using Microsoft.JSInterop;

namespace ProjectF.WebUI.Pages.Invoices
{
    public class InvoiceLinesHandler : ComponentBase
    {
        [Parameter]
        public Product[] ProductDataSource { get; set; }
        public string ProductLineId { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }

        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        protected override Task OnInitializedAsync()
        {
            ProductDataSource = System.Array.Empty<Product>();
            InvoiceLines = new List<InvoiceLine> { GetEmptyLine() };
            return Task.CompletedTask;
        }

        protected Product GetProduct(string searchText)
            => ProductDataSource
            .FirstOrDefault(p => p.Id == parseInt(searchText).Match(r => r, () => 0));

        protected readonly Func<string, SelectOption, bool> FilterOptionValue = FilterOption;

        protected static bool FilterOption(string value, SelectOption option)
            => option.Children.ToUpperInvariant()
            .Contains(value.ToLower(), StringComparison.OrdinalIgnoreCase);

        protected void OnChange(OneOf<string, IEnumerable<string>, LabeledValue, IEnumerable<LabeledValue>> value,
            OneOf<SelectOption, IEnumerable<SelectOption>> option, InvoiceLine line)
        {
            var product = GetProduct(value.Value.ToString());
                
            line.Product = product;
            line.Qty     = 1;
            line.IsEmpty = false;
            InvoiceLines.Add(GetEmptyLine());
            StateHasChanged();
        }

        public void ClearLines()
        {
            InvoiceLines.RemoveAll(l => l.Index >= 0);
            UpdateEmptyLines();
            ((IJSInProcessRuntime)jSRuntime).InvokeVoid("removeLine");
        }

        protected void SaveLine(InvoiceLine line)
        {
            line.IsEmpty = false;
            Console.WriteLine($"save line {Serialize(line)}");
            InvoiceLines.Add(GetEmptyLine());
        }

        protected void Remove(InvoiceLine line)
        {
            if (line.IsEmpty) return;
            line.IsDelete = true;
            ((IJSInProcessRuntime)jSRuntime).InvokeVoid("removeLine");   
        }

        InvoiceLine GetEmptyLine()
            => new InvoiceLine() { Product = new Product(), IsEmpty = true, Index = InvoiceLines.Count + 1 };

        void UpdateEmptyLines()
        {
            var shouldAddLines = InvoiceLines.Count - InvoiceLines.Count(i => i.Product?.Id != 0) == 1;
            if (shouldAddLines) return;

            var line = GetEmptyLine();
            InvoiceLines.Add(line);
        }

    }

}
