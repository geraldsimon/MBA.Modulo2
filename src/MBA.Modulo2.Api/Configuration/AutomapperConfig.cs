using AutoMapper;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Data.Domain;
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
            CreateMap<Produto, ProdutoLoggedOutViewModel>().ReverseMap();
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Comentario, ComentarioViewModel>().ReverseMap();
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Favorito, FavoritoViewModel>().ReverseMap();
            CreateMap<Favorito, FavoritoDoClienteViewModel>();

            CreateMap<Produto, ProdutoDetalhesViewModel>()
                .ForMember(dest => dest.Vendedor, opt => opt.MapFrom(src => src.Vendedor));

            CreateMap<Vendedor, VendedorViewModel>()
                .ForMember(dest => dest.produtoReduzidos, opt => opt.MapFrom(src => src.Produtos))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}