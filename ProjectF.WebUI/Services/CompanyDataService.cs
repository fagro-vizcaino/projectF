using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ProjectF.WebUI.Pages.Company;

namespace ProjectF.WebUI.Services
{
    public class CompanyDataService : _BaseDataService<Company>
    {
        const string baseUrl = "config/company";
        public CompanyDataService(HttpClient httpClient)
            : base(baseUrl, httpClient) { }
    }
}
