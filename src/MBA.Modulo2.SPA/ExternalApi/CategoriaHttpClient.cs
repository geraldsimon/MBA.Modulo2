using Blazored.LocalStorage;
using MBA.Modulo2.Spa.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MBA.Modulo2.Spa.ExternalApi
{
    public class CategoriaHttpClient(HttpClient httpClient, ILocalStorageService localStorage)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorage= localStorage;
        private readonly string _api = "api/categoria";
        
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<CategoriaViewModel>> GetCategoriasAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_api}?api-version=1.0");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
   
            var cliente = JsonSerializer.Deserialize<List<CategoriaViewModel>>(json, _jsonSerializerOptions);

            return cliente ?? [];
        }

        public async Task<CategoriaViewModel> GetCategoriaByIdAsync(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_api}/{Id}?api-version=1.0");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
     
            var cliente = JsonSerializer.Deserialize<CategoriaViewModel>(json, _jsonSerializerOptions);

            return cliente ?? new CategoriaViewModel();
        }
    }
}