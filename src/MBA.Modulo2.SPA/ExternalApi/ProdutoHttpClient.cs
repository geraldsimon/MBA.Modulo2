using MBA.Modulo2.Spa.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MBA.Modulo2.Spa.ExternalApi
{
    public class ProdutoHttpClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _api = "api/Produto";

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<ProdutoLoggedOutViewModel>> GetProdutosAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_api}?api-version=1.0");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<List<ProdutoLoggedOutViewModel>>(json, _jsonSerializerOptions);

            return products ?? [];
        }

        public async Task<ProdutoDetalhesViewModel> GetProdutoDetalheAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_api}/{id }/detalhes?api-version=1.0");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<ProdutoDetalhesViewModel>(json, _jsonSerializerOptions);

            return products ?? new ProdutoDetalhesViewModel();
        }
    }
}
