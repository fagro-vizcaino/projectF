using AntDesign;
using LanguageExt;
using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Models;
using System;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Invoices
{
    public class InvoiceLinesHandler : ComponentBase
    {
        [Parameter]
        public Product[] Products { get; set; }
        public string ProductLineId { get; set; }
        public Product LineProduct { get; set; }
        public int Qty { get; set; }

        protected override Task OnInitializedAsync()
        {
            Products = System.Array.Empty<Product>();
            LineProduct = new Product();
            Qty = 0;
            return base.OnInitializedAsync();
        }

        protected void OnSelectionChange(AutoCompleteOption item)
        {
            Console.WriteLine($"value here {System.Text.Json.JsonSerializer.Serialize(item?.Value)}");
            LineProduct =  item.Value switch
            {
                Product _ => (Product) item.Value,
               _ => new Product()
            };
            
        }
    }
}
