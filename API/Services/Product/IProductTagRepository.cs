using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Entities;

namespace API.Services
{
    public interface IProductTagRepository
    {
        IQueryable<Tag> GetTagsByProductId(int productId);

        Tag GetTagById(int id);
    }
}