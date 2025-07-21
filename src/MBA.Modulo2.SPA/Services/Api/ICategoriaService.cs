using MBA.Modulo2.Spa.Models.DTOs;

namespace MBA.Modulo2.Spa.Services.Api
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> GetCategorias();
        Task<CategoriaDTO> GetCategoria(Guid id);

    }
}
