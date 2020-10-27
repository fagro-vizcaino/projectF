using ProjectF.WebUI.Pages.Invoices;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class InvoiceDataService : _BaseDataService<Invoice>
    {
        const string baseUrl = "sales/invoice";

        public InvoiceDataService(HttpClient client) 
            : base(baseUrl, client) { }
    }
}
