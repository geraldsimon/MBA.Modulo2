using Blazored.LocalStorage;
using MBA.Modulo2.Spa.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MBA.Modulo2.Spa.Services.Autentica
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;  
        private readonly ILocalStorageService _localStorage;

        public AuthService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorageService;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiSpa");
                var loginJson = JsonSerializer.Serialize(loginModel);
                var requestContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/Auth/login",requestContent);

                var json = await response.Content.ReadAsStringAsync();

                var loginResult = JsonSerializer.Deserialize<LoginResult>(
                    json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (!response.IsSuccessStatusCode)
                {
                    return loginResult;
                }

                if (loginResult is not null && loginResult.Success)
                {
                    var token = loginResult.Data.AccessToken;
                    var expiration = DateTime.UtcNow.AddSeconds(loginResult.Data.ExpiresIn);

                    await _localStorage.SetItemAsync("authToken", token);
                    await _localStorage.SetItemAsync("tokenExpiration", expiration);
                }

                ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                                    .MarkUserAsAuthenticated(loginModel.Email);

                httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("bearer",
                                                             loginResult.Data.AccessToken);

                return loginResult;
            }
            catch (Exception)
            {

                throw;
            } 
        }

        public async Task Logout()
        {
            var httpClient = _httpClientFactory.CreateClient("ApiSpa");
            await _localStorage.RemoveItemAsync("authToken");

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
