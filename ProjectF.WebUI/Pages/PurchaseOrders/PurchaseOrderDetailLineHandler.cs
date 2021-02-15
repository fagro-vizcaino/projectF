using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Pages.Products;

namespace ProjectF.WebUI.Pages.PurchaseOrders
{
    public class PurchaseOrderDetailLineHandler : ComponentBase
    {
        [Parameter]
        public Product[] Products { get; set; } = System.Array.Empty<Product>();
        
        [Parameter]
        public PaymentTerm[] PaymentTerms { get; set; } = System.Array.Empty<PaymentTerm>();

        [Parameter]
        public Tax[] Taxes { get; set; } = System.Array.Empty<Tax>();
        public ImmutableList<PurchaseOrderDetail> Details { get; set; } = ImmutableList<PurchaseOrderDetail>.Empty;

        public PurchaseOrderDetail Line { get; set; }
        public int SelectedProduct { get; set; } = 0;

        protected override Task OnInitializedAsync()
        {
            Line = new();
            return base.OnInitializedAsync();
        }


        public void OnSelectProductChangedHandler(Product product)
        {
            Console.WriteLine($"product selected {product.Name}");
        }

        public void OnAddPurchaseDetailHandler()
        {

        }
    }
}
