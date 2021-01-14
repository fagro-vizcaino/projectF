using System.Net.Http;
using ProjectF.WebUI.Pages.UnitOfMeasures;

namespace ProjectF.WebUI.Services
{
    public class UnitOfMeasureDataService : _BaseDataService<UnitOfMeasure>
    {
        const string _baseUrl = "config/unitofmeasure";

        public UnitOfMeasureDataService(HttpClient httpClient) 
            : base(_baseUrl, httpClient) { }
    }
}
