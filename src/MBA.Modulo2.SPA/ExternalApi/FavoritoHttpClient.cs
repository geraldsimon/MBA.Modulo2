using System.Net.Http.Headers;
using System.Text.Json;
using MBA.Modulo2.Spa.ViewModels;

namespace MBA.Modulo2.Spa.ExternalApi
{
    public class FavoritoHttpClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _api = "api/Favorito";

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };


        public async Task<List<FavoritoDoClienteViewModel>> PegarosFavoritos(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_api}/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var favoritos = JsonSerializer.Deserialize<List<FavoritoDoClienteViewModel>>(json, _jsonSerializerOptions);

            return favoritos ?? new List<FavoritoDoClienteViewModel>();
        }








    }
}
