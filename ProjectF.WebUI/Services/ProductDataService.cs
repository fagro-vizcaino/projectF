using System.Net.Http;
using ProjectF.WebUI.Pages.Products;

namespace ProjectF.WebUI.Services
{
    public class ProductDataService : BaseDataService<Product>
    {
        private const string BaseUrl = "inventory/product";
        public ProductDataService(HttpClient httpClient) 
            : base(BaseUrl, httpClient) {}
    }
}
