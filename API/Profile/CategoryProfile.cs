using API.Entities;
using API.Models;

namespace API.Profile
{
    public class CategoryProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Map Category entity to Category models
        /// </summary>
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name)
                ).ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description)
                );
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<CategoryDto, Category>();
        }
    }
}