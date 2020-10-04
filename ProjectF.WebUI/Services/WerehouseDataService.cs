using ProjectF.WebUI.Models;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class WerehouseDataService : _BaseDataService<Werehouse>
    {
        const string _baseUrl = "inventory/werehouse";

        public WerehouseDataService(HttpClient httpClient)
            : base(_baseUrl, httpClient) { }
    }
}
