using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Pages.Products;

namespace ProjectF.WebUI.Pages.PurchaseOrders
{
    public class PurchaseOrderDetailLineHandler : ComponentBase
    {
        [Parameter] public Product[] Products { get; set; } = Array.Empty<Product>();
        [Parameter] public EventCallback<PurchaseOrderDetail> OnSaveDetailClick { get; set; }

        [Parameter] public PaymentTerm[] PaymentTerms { get; set; } = Array.Empty<PaymentTerm>();
       
        [Parameter] public Tax[] Taxes { get; set; } = Array.Empty<Tax>();
        public ImmutableList<PurchaseOrderDetail> Details { get; set; } = ImmutableList<PurchaseOrderDetail>.Empty;

        public PurchaseOrderDetail Line { get; set; }
        public int SelectedProduct { get; set; } = 0;

        protected override Task OnInitializedAsync()
        {
            Line = new();
            return base.OnInitializedAsync();
        }

        protected void OnSelectProductChangedHandler(Product product)
        {
            Line = new PurchaseOrderDetail()
            {
                Cost = product.Cost,
                Description = product.Name,
                ProductCode = product.Code,
                Qty = 1,
                TaxPercent = Line.TaxPercent
            };
        }
        protected void OnSaveHandler()
        {
            OnSaveDetailClick.InvokeAsync(Line);
            Line = new();
        }

        
    }
}
