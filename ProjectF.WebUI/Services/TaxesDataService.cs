using ProjectF.WebUI.Models;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class TaxesDataService : BaseDataService<Tax>
    {
        const string _baseUrl = "config/tax";

        public TaxesDataService(HttpClient httpClient)
            : base(_baseUrl, httpClient) { }
    }
}
