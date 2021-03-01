using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ProjectF.WebUI.Pages.Invoices.List;
using ProjectF.WebUI.Services.Common;
using Array = System.Array;

namespace ProjectF.WebUI.Services
{
    public class BaseDataService<T> : IBaseDataService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        protected BaseDataService(string serviceUrl, HttpClient httpClient)
            => (_baseUrl, _httpClient) = ($"{httpClient.BaseAddress}{serviceUrl}", httpClient);
        
        public async Task<Option<T>> Add(T element)
        {
            var elementJson =
                new StringContent(JsonSerializer.Serialize(element), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, elementJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<Option<T>> Update(long id, T element)
        {
            var elementJson =
                new StringContent(JsonSerializer.Serialize(element), Encoding.UTF8, "application/json");

            var result = await _httpClient.PutAsync($"{_baseUrl}/{id}", elementJson);
            return result.IsSuccessStatusCode 
                ? Some(await result.Content.ReadFromJsonAsync<T>())
                : None;
        }

        public async Task<Option<string>> Delete(long elementId)
        {
            var result = await _httpClient.DeleteAsync($"{_baseUrl}/{elementId}");
            return result.IsSuccessStatusCode
                ? Some(Boolean.TrueString)
                : None;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                var result =  await _httpClient.GetFromJsonAsync<IEnumerable<T>>(_baseUrl);
                var items = result as T[] ?? (result ?? Array.Empty<T>()).ToArray();
                //Console.WriteLine($"Items from {JsonSerializer.Serialize(items)}");
                return items;
            }
            catch (Exception ex)
            {
                //Todo: Log propertly errors
                Console.WriteLine($"Error log:\n{ex.Message}\n{ex.StackTrace}");
            }
            return Enumerable.Empty<T>();

        }

        public virtual async Task<IEnumerable<T>> Get(RequestQueryParametersBase requestQuery)
        {
            try
            {
                var queryParameters = requestQuery.GetRequestQueryString() ?? "";
                //Console.WriteLine($"request query: {_baseUrl}{queryParameters}");
                
                var result = await _httpClient
                    .GetFromJsonAsync<IEnumerable<T>>($"{_baseUrl}{queryParameters}", CancellationToken.None);

                var items = result as T[] ?? (result ?? Array.Empty<T>()).ToArray();
                //Console.WriteLine($"Items from {JsonSerializer.Serialize(items)}");
                return items;
            }
            catch (Exception ex)
            {
                //Todo: Log propertly errors
                Console.WriteLine($"Error log:\n{ex.Message}\n{ex.StackTrace}");
            }
            return Enumerable.Empty<T>();
        }


        public async Task<T> GetDetails(long elementId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>($"{_baseUrl}/{elementId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error log:\n{ex.Message}\n{ex.StackTrace}");
            }
            return null;
        }
    }
}
