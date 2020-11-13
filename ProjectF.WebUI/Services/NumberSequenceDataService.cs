using ProjectF.WebUI.Pages.NumberSequences;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class NumberSequenceDataService : _BaseDataService<NumberSequence>
    {
        const string _baseUrl = "settings/numberSequence";
        public NumberSequenceDataService(HttpClient httpClient) 
            : base(_baseUrl, httpClient) { }
    }
}
