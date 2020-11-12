using ProjectF.WebUI.Models;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class BankAccountTypeDataService : _BaseDataService<BankAccountType>
    {
        const string _baseUrl = "finance/bankAccountType";
        public BankAccountTypeDataService(HttpClient httpClient) 
            : base(_baseUrl, httpClient) { }
    }
}
