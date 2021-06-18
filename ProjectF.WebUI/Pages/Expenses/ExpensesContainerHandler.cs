using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ProjectF.WebUI.Pages.Expenses
{
    public class ExpensesContainerHandler : ComponentBase
    {
        public ExpenseContainerTabs TabSelected { get; set; }
        public string Active { get; set; }
        protected override Task OnInitializedAsync()
        {
            TabSelected = ExpenseContainerTabs.Expenses;
            return base.OnInitializedAsync();
        }

        public void SetTab(ExpenseContainerTabs selectedTab)
            => TabSelected = selectedTab;

        public string ActiveTab(ExpenseContainerTabs selected)
            => TabSelected == selected ? "text-blue-500 border-b-2 font-medium border-blue-500" : "";
    }

    public enum ExpenseContainerTabs
    {
        Expenses,
        SupplierInvoices,
        PurchaseOrders,
        ProductsAndServices
    }
}
