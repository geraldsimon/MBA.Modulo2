using Blazored.LocalStorage;
using MBA.Modulo2.Spa.Models;
using MBA.Modulo2.Spa.Services.Autenticacao;
using MBA.Modulo2.Spa.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MBA.Modulo2.Spa.ExternalApi
{
    public class AuthHttpClient(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;
        private readonly string _api = "api/Auth";

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<LoginModel> LoginAsync(LoginViewModel loginModel)
        {
            var loginJson = JsonSerializer.Serialize(loginModel);
            var requestContent = new StringContent(loginJson, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"{_api}/login?api-version=1.0", requestContent);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var loginResult = JsonSerializer.Deserialize<LoginModel>(json, _jsonSerializerOptions);

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

        public async Task<LoginModel> NovoCadastroAsync(CadastroUserModel cadastroUserModel)
        {
            var loginJson = JsonSerializer.Serialize(cadastroUserModel);
            var requestContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_api}/newAccount", requestContent);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var loginResult = JsonSerializer.Deserialize<LoginModel>(json, _jsonSerializerOptions);

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
                                .MarkUserAsAuthenticated(cadastroUserModel.Email);

            httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("bearer",
                                                     loginResult.Data.AccessToken);
            return loginResult;
        }


        public async Task<Guid?> PegarIdUsuario()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            Console.WriteLine($"IsAuthenticated: {user.Identity?.IsAuthenticated}");

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                // Busca o claim do tipo "sub", que contém o ID do usuário
                var idClaim = user.FindFirst("sub");

                Console.WriteLine($"Claim 'sub': {idClaim?.Value}");

                if (idClaim is not null && Guid.TryParse(idClaim.Value, out Guid userId))
                {
                    return userId;
                }
            }

            return null;
        }


    }
}