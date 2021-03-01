using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ProjectF.WebUI.Pages.PurchaseOrders;

namespace ProjectF.WebUI.Services
{
    public class PurchaseOrderDataService : BaseDataService<PurchaseOrder>
    {
        const string _baseUrl = "expenses/PurchaseOrders";
        public PurchaseOrderDataService(HttpClient httpClient) 
            : base(_baseUrl, httpClient) { }
    }
}
