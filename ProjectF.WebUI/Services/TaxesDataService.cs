using ProjectF.WebUI.Models;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class TaxesDataService : _BaseDataService<Tax>
    {
        const string _baseUrl = "config/tax";

        public TaxesDataService(HttpClient httpClient)
            : base(_baseUrl, httpClient) { }
    }
}
