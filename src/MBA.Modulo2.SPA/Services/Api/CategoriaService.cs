using MBA.Modulo2.Spa.Models.DTOs;
using System.Net.Http.Json;

namespace MBA.Modulo2.Spa.Services.Api
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ILogger<CategoriaService> _Logger;
        private const string apiEndPoint = "/api/categoria";

        private CategoriaDTO? categoria;
        private IEnumerable<CategoriaDTO>? Categorias;

        public CategoriaService(IHttpClientFactory httpClientFactory,
            ILogger<CategoriaService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _Logger = logger;
        }

        public async Task<CategoriaDTO> GetCategoria(Guid id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiSpa");
                var response = await httpClient.GetAsync(apiEndPoint + id);

                if (response.IsSuccessStatusCode)
                {
                    categoria = await response.Content.ReadFromJsonAsync<CategoriaDTO>();
                    return categoria;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _Logger.LogError($"Erro ao obter a categoria pelo id= {id} - {message}");
                    throw new Exception($"Status Code:{response.StatusCode}-{message}");
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Erro ao  obter a categoria pelo id ={id} \n \n {ex.Message}");
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<List<CategoriaDTO>> GetCategorias()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiSpa");
                var result = await httpClient.GetFromJsonAsync<List<CategoriaDTO>>(apiEndPoint);
                return result;
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Erro ao acessar categorias:{apiEndPoint}" + ex.Message);
                throw new UnauthorizedAccessException();
            }
        }
    }
}
