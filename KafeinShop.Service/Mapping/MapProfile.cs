using AutoMapper;
using KafeinShop.Core.DTOs;
using KafeinShop.Core.Model;

namespace KafeinShop.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryWithProductsDto>();
            CreateMap<User, UserDto>().ReverseMap(); 


        }
    }
}
