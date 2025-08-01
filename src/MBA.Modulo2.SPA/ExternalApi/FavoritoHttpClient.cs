using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MBA.Modulo2.Spa.ViewModels;
using MBA.Modulo2.Spa.ExternalApi;
using Blazored.LocalStorage;

namespace MBA.Modulo2.Spa.ExternalApi
{
    public class FavoritoHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthHttpClient _authHttpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly string _api = "api/Favorito";

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public FavoritoHttpClient(HttpClient httpClient, AuthHttpClient authHttpClient, ILocalStorageService localStorage)
        {
            this._httpClient = httpClient;
            this._authHttpClient = authHttpClient;
            this._localStorage = localStorage;
        }

        private async Task<Guid> GetIdClienteAsync()
        {
            var idCliente = await _authHttpClient.PegarIdCliente();
            if (idCliente == null)
                throw new InvalidOperationException("Usuário não autenticado ou clienteId não encontrado.");
            return idCliente.Value;
        }


        public async Task<List<FavoritoDoClienteViewModel>> PegarosFavoritos()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");


            var request = new HttpRequestMessage(HttpMethod.Get, $"{_api}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }


            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var favoritos = JsonSerializer.Deserialize<List<FavoritoDoClienteViewModel>>(json, _jsonSerializerOptions);

            return favoritos ?? new List<FavoritoDoClienteViewModel>();
        }

        public async Task<bool> DeletarFavorito(Guid idProduto)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_api}/{idProduto}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);
            if(response.StatusCode == HttpStatusCode.NoContent)
                return true;

            return false;
        }

        public async Task<byte> RegistrarFavorito(Guid idProduto)
        {
            var body = new
            {
                produtoId = idProduto
            };

            var token = await _localStorage.GetItemAsync<string>("authToken");


            var jsonBody = JsonSerializer.Serialize(body, _jsonSerializerOptions);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_api}?api-version=1.0")
            {
                Content = content
            };
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
                return 1;
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                bool deletar = await DeletarFavorito(idProduto);
                if (deletar == false)
                    return 3;
                return 2;
            }
            return 3;


        }










    }
}
