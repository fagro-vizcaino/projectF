using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Sales
{
    public class SalesContainerHandler : ComponentBase
    {
        public SalesContainerTabs TabSelected { get; set; }
        public string Active { get; set; }
        protected override Task OnInitializedAsync()
        {
            TabSelected = SalesContainerTabs.Sales;
            return base.OnInitializedAsync();
        }

        public void SetTab(SalesContainerTabs selectedTab)
            => TabSelected = selectedTab;

        public string ActiveTab(SalesContainerTabs selected) 
            => TabSelected == selected ? "text-blue-500 border-b-2 font-medium border-blue-500" : "";
    }

    public enum SalesContainerTabs
    {
        Sales,
        Invoices,
        Customer,
        ProductsAndServices
    }
}
