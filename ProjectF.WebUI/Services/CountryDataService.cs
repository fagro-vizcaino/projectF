using ProjectF.WebUI.Models;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class CountryDataService : _BaseDataService<Country>
    {
        const string _baseUrl = "common/country";

        public CountryDataService(HttpClient httpClient)
            : base(_baseUrl, httpClient) { }

    }
}
