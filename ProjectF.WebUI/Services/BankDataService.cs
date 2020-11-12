using ProjectF.WebUI.Models;
using System.Net.Http;
using ProjectF.WebUI.Pages;

namespace ProjectF.WebUI.Services
{
    public class BankDataService : _BaseDataService<Bank>
    {
        const string _baseUrl = "finance/bankAccount";

        public BankDataService(HttpClient httpClient)
            : base(_baseUrl, httpClient) { }
    }
}
