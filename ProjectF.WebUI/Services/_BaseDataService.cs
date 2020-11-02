using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProjectF.WebUI.Pages.Invoices.List;
using ProjectF.WebUI.Services.Common;

namespace ProjectF.WebUI.Services
{
    public class _BaseDataService<T> : IBaseDataService<T> where T : class
    {
        readonly HttpClient _httpClient;
        readonly string baseUrl = string.Empty;

        public _BaseDataService(string serviceUrl, HttpClient httpClient)
            => (baseUrl, _httpClient) = ($"{httpClient.BaseAddress}{serviceUrl}", httpClient);
        
        public async Task<Option<T>> Add(T element)
        {
            var elementJson =
                new StringContent(JsonSerializer.Serialize(element), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl, elementJson);

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

            var result = await _httpClient.PutAsync($"{baseUrl}/{id}", elementJson);
            return result.IsSuccessStatusCode 
                ? Some(await result.Content.ReadFromJsonAsync<T>())
                : None;
        }

        public async Task<Option<string>> Delete(long elementId)
        {
            var result = await _httpClient.DeleteAsync($"{baseUrl}/{elementId}");
            return result.IsSuccessStatusCode
                ? Some(Boolean.TrueString)
                : None;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                var result =  await _httpClient.GetFromJsonAsync<IEnumerable<T>>(baseUrl);
                Console.WriteLine($"Items from {JsonSerializer.Serialize(result)}");
                return result;
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
                var queryParameters = (requestQuery.GetRequestQueryString() ?? "");
                Console.WriteLine($"request query: {baseUrl}{queryParameters}");
                var result = await _httpClient.GetFromJsonAsync<IEnumerable<T>>($"{baseUrl}{queryParameters}");
                Console.WriteLine($"Items from {JsonSerializer.Serialize(result)}");
                return result;
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
                return await _httpClient.GetFromJsonAsync<T>($"{baseUrl}/{elementId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error log:\n{ex.Message}\n{ex.StackTrace}");
            }
            return null;
        }
    }
}
