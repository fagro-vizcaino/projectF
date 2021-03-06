﻿using System.Net.Http;
using ProjectF.WebUI.Pages.UnitOfMeasures;

namespace ProjectF.WebUI.Services
{
    public class UnitOfMeasureDataService : BaseDataService<UnitOfMeasure>
    {
        const string _baseUrl = "config/UnitOfMeasure";

        public UnitOfMeasureDataService(HttpClient httpClient) 
            : base(_baseUrl, httpClient) { }
    }
}
