using AutoMapper;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Data.Models;
using MBA.Modulo2.Shared.ViewModels;

namespace MBA.Modulo2.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Produto, ProductLoggedOutViewModel>().ReverseMap();
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Comentario, ComentarioViewModel>().ReverseMap();
        }
    }
}