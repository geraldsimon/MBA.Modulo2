using AutoMapper;
using MBA.Modulo2.Spa.Models.DTOs;
using MBA.Modulo2.Spa.ViewModels;

namespace MBA.Modulo2.Spa.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            //CreateMap<ProdutoLoggedOutDto, ProdutoDetalhesViewModel>().ReverseMap();
        }
    }
}