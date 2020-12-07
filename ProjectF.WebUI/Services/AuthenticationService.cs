using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProjectF.WebUI.Pages.Auth;

namespace ProjectF.WebUI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly HttpClient _client;
        const string _baseUrl = "authentication/register";
        
        public AuthenticationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserRegisterDto dto)
        {
            var serverUrl = $"{_client.BaseAddress}{_baseUrl}";

            var elementJson =
               new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            Console.WriteLine($"register : { await elementJson.ReadAsStringAsync() }");
            var response = await _client.PostAsync(serverUrl, elementJson);

            var registrationContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer
                    .Deserialize<RegistrationResponseDto>(registrationContent
                    , new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }

            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }

        public async Task<int> ConfirmedEmail(string token, string email)
        {
            const string EMAILCONFIRMED = "authentication/confirmemail";
            var serverUrl = $"{_client.BaseAddress}{EMAILCONFIRMED}?token={token}&email={email}";

            Console.WriteLine($"confirmed email : { serverUrl }");
            var response = await _client.GetAsync(serverUrl);

            var registrationContent = await response.Content.ReadAsStringAsync();
            const int noneResult = 0;
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer
                    .Deserialize<int>(registrationContent
                    , new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return noneResult;
        }
    }
}
