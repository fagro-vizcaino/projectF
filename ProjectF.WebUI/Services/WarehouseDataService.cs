using ProjectF.WebUI.Models;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class WarehouseDataService : _BaseDataService<Warehouse>
    {
        const string _baseUrl = "config/warehouse";

        public WarehouseDataService(HttpClient httpClient)
            : base(_baseUrl, httpClient) { }
    }
}
