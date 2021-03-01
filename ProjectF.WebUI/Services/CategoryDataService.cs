using ProjectF.WebUI.Models;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class CategoryDataService : BaseDataService<Category>
    {
        const string _baseUrl = "config/category";

        public CategoryDataService(HttpClient httpClient) 
            : base(_baseUrl, httpClient) { }
     
    }
}
