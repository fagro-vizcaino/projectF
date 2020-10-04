using ProjectF.WebUI.Models;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class PaymentTermDataService : _BaseDataService<PaymentTerm>
    {
        const string _baseUrl = "config/paymentterm";
        public PaymentTermDataService(HttpClient httpClient)
            : base(_baseUrl, httpClient) { }
    }
}
