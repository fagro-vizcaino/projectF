using ProjectF.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Services
{
    public class BankAccountTypeDataService : _BaseDataService<BankAccountType>
    {
        const string _baseUrl = "finance/bankAccountType";
        public BankAccountTypeDataService(HttpClient httpClient) 
            : base(_baseUrl, httpClient) { }
    }
}
