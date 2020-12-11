﻿using System;
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

        public async Task<AuthReponseDto> SignIn(UserLoginDto userLoginDto)
        {
            //const string SignInUrl = "authentication/login";
            //var serverUrl = $"{_client.BaseAddress}{SignInUrl}";

            //var content = JsonSerializer.Serialize(userLoginDto);
            //var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            //var authResult = await _client.PostAsync("https://localhost:5011/api/accounts/login", bodyContent);
            //var authContent = await authResult.Content.ReadAsStringAsync();
            
            //var result = JsonSerializer.Deserialize<Auth>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //if (!authResult.IsSuccessStatusCode)
            //    return result;
            //await _localStorage.SetItemAsync("authToken", result.Token);
            
            //((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            //return new AuthResponseDto { IsAuthSuccessful = true };
            return new();
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
            var serverUrl = $"{_client.BaseAddress}{EMAILCONFIRMED}?token={token.Trim()}&email={email.Trim()}";

            Console.WriteLine($"confirmed email : { serverUrl }");
            var response = await _client.GetAsync(serverUrl);

            var registrationContent = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
            {
                var result = JsonSerializer
                    .Deserialize<int>(registrationContent
                    , new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return response.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<bool> ForgotPassword(UserForgotPasswordDto dto)
        {
            const string currentBaseUrl = "authentication/forgotpassword";
            var serverUrl = $"{_client.BaseAddress}{currentBaseUrl}";

            var elementJson =
               new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            Console.WriteLine($"forgot password : { await elementJson.ReadAsStringAsync() }");
            var response = await _client.PostAsync(serverUrl, elementJson);

           return response.IsSuccessStatusCode;
        }

        public async Task<bool> ResetPassword(string token, string email, UserResetPasswordDto dto)
        {
            const string EMAILCONFIRMED = "authentication/resetpassword";
            var serverUrl = $"{_client.BaseAddress}{EMAILCONFIRMED}?token={token.Trim()}&email={email.Trim()}";

            var elementJson =
               new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            Console.WriteLine($"forgot password : { await elementJson.ReadAsStringAsync() }");
            var response = await _client.PostAsync(serverUrl, elementJson);

            return response.IsSuccessStatusCode;
        }
    }
}