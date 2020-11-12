using ProjectF.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ProjectF.WebUI.Pages.Products;

namespace ProjectF.WebUI.Services
{
    public class ProductDataService : _BaseDataService<Product>
    {
        const string baseUrl = "inventory/product";
        public ProductDataService(HttpClient httpClient) 
            : base(baseUrl, httpClient) {}
    }
}
