using System;
using System.Collections.Generic;
using System.Linq;
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
        const string _baseUrl = "Authentication";
        public AuthenticationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserRegisterDto userForRegistration)
        {
            var elementJson =
               new StringContent(JsonSerializer.Serialize(userForRegistration)
               , Encoding.UTF8, "application/json");
            var serverUrl = $"{_client.BaseAddress}{_baseUrl}";

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
    }
}
