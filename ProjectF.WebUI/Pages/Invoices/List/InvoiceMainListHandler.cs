using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Invoices.List
{
    public class InvoiceMainListHandler : ComponentBase
    {
        public bool DropDownDateOpen { get; set; } = false;
        public bool DropDownOpen { get; set; } = false;
        public bool DropDownStatusOpen { get; set; } = false;

        public bool DropDownEditOpen { get; set; } = false;
        public InvoiceListParameters QueryParameters { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IBaseDataService<InvoiceMainList> InvoiceService { get; set; }
        public InvoiceMainList[] MainLists { get; set;} = Array.Empty<InvoiceMainList>();
        protected override async Task OnInitializedAsync()
        {
            QueryParameters = new InvoiceListParameters();
            MainLists = (await InvoiceService.Get(QueryParameters)).ToArray();
        }

        public async Task OnFilterDateHandler(DateTime[] dates)
        {
            var (start, end) = dates.Length switch
            {
                1 => (start: dates[0], end: DateTime.Today),
                var n when (n > 1) => (start: dates[0], end: dates.ElementAtOrDefault(1)),
                _ => (start: DateTime.Today, end: DateTime.Today.AddDays(-30))
            };
            
            Console.WriteLine($"start {start:yyyy-MM-dd} and end {end:yyyy-MM-dd}");
            QueryParameters.DateFrom = start;
            QueryParameters.DateTo = end;
            MainLists = (await InvoiceService.Get(QueryParameters)).ToArray();
        }

        public void GoToPage(string path) => NavigationManager.NavigateTo($"{path}");

    
    }
}
