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
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, ProductLoggedOutViewModel>().ReverseMap();
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
        }
    }
}