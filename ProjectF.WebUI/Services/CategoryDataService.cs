using ProjectF.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Services
{
    public class CategoryDataService : _BaseDataService<Category>
    {
        const string _baseUrl = "inventory/category";

        public CategoryDataService(HttpClient httpClient) 
            : base(_baseUrl, httpClient) { }
     
    }
}
