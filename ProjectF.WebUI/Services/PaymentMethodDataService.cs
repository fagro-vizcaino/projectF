using System.Net.Http;
using ProjectF.WebUI.Pages.PaymentMethods;

namespace ProjectF.WebUI.Services
{
    public class PaymentMethodDataService : _BaseDataService<PaymentMethod>
    {
        const string _baseUrl = "config/paymentMethod";
        public PaymentMethodDataService(HttpClient httpClient)
            : base(_baseUrl, httpClient) { }
    }
}
