using API.Entities;
using API.Models;

namespace API.Profile
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name)
                ).ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description)
                ).ForMember(
                    dest => dest.AffiliateLink,
                    opt => opt.MapFrom(src => src.AffiliateLink)
                ).ForMember(
                    dest => dest.ImageSource,
                    opt => opt.MapFrom(src => src.ImageSource)
                ).ForMember(
                    dest => dest.ProductUrl,
                    opt => opt.MapFrom(src => src.ProductUrl)
                )
                .ForMember(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.CategoryId)
                );

            CreateMap<ProductForCreationDto, Product>();
        }
    }
}