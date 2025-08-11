using Blazored.LocalStorage;
using MBA.Modulo2.Spa.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MBA.Modulo2.Spa.ExternalApi
{
    public class ClienteHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthHttpClient _authHttpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly string _api = "api/Cliente";

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ClienteHttpClient(HttpClient httpClient, AuthHttpClient authHttpClient, ILocalStorageService localStorage)
        {
            this._httpClient = httpClient;
            this._authHttpClient = authHttpClient;
            this._localStorage = localStorage;
        }

        public async Task<ClienteViewModel> GetClienteAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_api}?api-version=1.0");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var cliente = JsonSerializer.Deserialize<ClienteViewModel>(json, options);

            return cliente ?? new ClienteViewModel();
        }

        public async Task<ProdutoDetalhesViewModel> GetClienteByIdAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_api}/{id}?api-version=1.0");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var products = JsonSerializer.Deserialize<ProdutoDetalhesViewModel>(json, options);

            return products ?? new ProdutoDetalhesViewModel();
        }

        public async Task<ClienteViewModel> ObterPorIdAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_api}/{id}?api-version=1.0");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var cliente = JsonSerializer.Deserialize<ClienteViewModel>(json, options);

            return cliente ?? new ClienteViewModel();
        }

        public async Task AtualizarClienteAsync(ClienteViewModel cliente)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            var json = JsonSerializer.Serialize(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Monta a URL incluindo o ID na rota (como o controller exige)
            var url = $"{_api}/{cliente.Id}?api-version=1.0";

            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = content
            };

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
