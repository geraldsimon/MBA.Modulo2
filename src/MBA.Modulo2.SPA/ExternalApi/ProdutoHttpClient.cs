using MBA.Modulo2.Spa.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MBA.Modulo2.Spa.ExternalApi
{
    public class ProdutoHttpClient
    {
        private readonly HttpClient _httpClient;

        public ProdutoHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductLoggedOutViewModel>> GetProdutosAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Product?api-version=1.0");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // Fixing CS0121 by explicitly specifying JsonSerializerOptions
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var products = JsonSerializer.Deserialize<List<ProductLoggedOutViewModel>>(json, options);

            return products ?? new List<ProductLoggedOutViewModel>();
        }

        public async Task<ProdutoDetalhesViewModel> GetProdDetalhes(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Product/" + id + "/detalhes?api-version=1.0");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // Fixing CS0121 by explicitly specifying JsonSerializerOptions
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var products = JsonSerializer.Deserialize<ProdutoDetalhesViewModel>(json, options);

            return products ?? new ProdutoDetalhesViewModel();
        }
    }
}
