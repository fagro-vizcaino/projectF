using AntDesign;
using LanguageExt;
using Microsoft.AspNetCore.Components;
using static LanguageExt.Prelude;
using ProjectF.WebUI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.core.JsInterop.EventArg;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using OneOf;
using System.Text.Json;

namespace ProjectF.WebUI.Pages.Invoices
{
    public class InvoiceLinesHandler : ComponentBase
    {
        [Parameter]
        public Product[] ProductDataSource { get; set; }
        public string ProductLineId { get; set; }
        public Stack<Product> LineProducts { get; set;}
        public int Qty { get; set; }
        public bool IsSelected { get; set; } = false;

        protected override Task OnInitializedAsync()
        {
            ProductDataSource     = System.Array.Empty<Product>();
            LineProducts          = new Stack<Product>();
            Qty                   = 0;
            LineProducts.Push(new Product());
            return base.OnInitializedAsync();
        }

        protected Product GetProduct(string searchText)
            => ProductDataSource
            .FirstOrDefault(p => p.Id == parseInt(searchText).Match(r => r, () => 0));
        

        protected string _value;
        protected readonly Func<string, SelectOption, bool> FilterOptionValue = FilterOption;


        protected static bool FilterOption(string value, SelectOption option)
        {
            var optionContent = option.Children.ToUpperInvariant();
            return optionContent.Contains(value.ToLower(), StringComparison.OrdinalIgnoreCase);
        }

        protected void OnChange(OneOf<string, IEnumerable<string>, LabeledValue, IEnumerable<LabeledValue>> value, OneOf<SelectOption, IEnumerable<SelectOption>> option)
        {
            var LineProduct = GetProduct(value.Value.ToString());
           
            LineProducts.Push(LineProduct);
            StateHasChanged();
        }

    }
}
