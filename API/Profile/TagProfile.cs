using API.Entities;
using API.Models;

namespace API.Profile
{
    public class TagProfile :AutoMapper.Profile
    {
        
        /// <summary>
        /// Map Tag entities to Tag models
        /// </summary>
        public TagProfile()
        {
            CreateMap<Tag, TagDto>().ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)
            ).ForMember(
                dest => dest.ProductId,
                opt => opt.MapFrom(src => src.ProductId)
            );

            CreateMap<TagForCreationDto, Tag>();
            CreateMap<TagDto, Tag>();
        }
    }
}