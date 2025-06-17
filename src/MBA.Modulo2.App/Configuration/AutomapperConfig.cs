using AutoMapper;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.App.Configuration;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {

        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Product, ProductViewModel>().ReverseMap();
    }
}