using ProjectF.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Services
{
    public class ClientDataService : _BaseDataService<Client>
    {
        const string serviceUrl = "contact/client";
        public ClientDataService(HttpClient httpClient) 
            : base(serviceUrl, httpClient) { }
    }
}
