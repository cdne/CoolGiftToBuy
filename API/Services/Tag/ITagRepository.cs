using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Entities;
using API.Models;

namespace API.Services
{
    public interface ITagRepository
    {
        ICollection<Tag> GetAllTags();
        Tag GetTagById(int id);
        IQueryable<ProductDto> GetAllProductsByTag(int tagId);
    }
}