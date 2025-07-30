using AutoMapper;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Data.Models;
using MBA.Modulo2.Data.Domain;

namespace MBA.Modulo2.App.Configuration;

public class AutomapperConfig : Profile
{
	public AutomapperConfig()
	{
		CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
		
		CreateMap<Produto, ProdutoViewModel>().ReverseMap()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
            .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Preco))
            .ForMember(dest => dest.Estoque, opt => opt.MapFrom(src => src.Estoque))
            .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.Imagem))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo));

        CreateMap<Vendedor, VendedorViewModel>().ReverseMap();

		CreateMap<Denuncia, DenunciaViewModel>()
			.ForMember(dest => dest.ProdutoNome, opt => opt.MapFrom(src => src.Produto.Nome))
			.ForMember(dest => dest.DenuncianteEmail, opt => opt.MapFrom(src => src.Denunciante.Email))
			.ForMember(dest => dest.AdminResponsavelEmail,
				opt => opt.MapFrom(src => src.AdminResponsavel != null ? src.AdminResponsavel.Email : null))
			.ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

		CreateMap<CriarDenunciaViewModel, Denuncia>()
			.ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo));
	}
}