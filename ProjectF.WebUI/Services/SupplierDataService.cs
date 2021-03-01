using ProjectF.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Services
{
    public class SupplierDataService : BaseDataService<Supplier>
    {
        const string _baseUrl = "inventory/supplier";
        public SupplierDataService(HttpClient httpClient) 
            : base(_baseUrl, httpClient) { }
    }
}
