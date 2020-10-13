using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Entities;
using API.Models;

namespace API.Services
{
    public interface ITagRepository
    {
        /// <summary>
        /// Get all tags from database 
        /// </summary>
         /// <returns>Collection of Tag entities</returns>
        ICollection<Tag> GetAllTags();
        /// <summary>
        /// Get tag from database by id
        /// </summary>
        /// <param name="id">Tag id</param>
        /// <returns>Tag from database</returns>
        Tag GetTagById(int id);
        /// <summary>
        /// Add tag in database
        /// </summary>
        /// <param name="tag">Tag entity to add</param>
        void Add(Tag tag);
        /// <summary>
        /// Update tag in database
        /// </summary>
        /// <param name="id">Tag id to update</param>
        /// <param name="tag">Tag data to update</param>
        void Update(int id, Tag tag);
        /// <summary>
        /// Partially update tag in database
        /// </summary>
        /// <param name="tag">Tag entity to update</param>
        void PartiallyUpdate(Tag tag);
        /// <summary>
        /// Delete tag from database
        /// </summary>
        /// <param name="tag">Tag entity to delete</param>
        void Delete(Tag tag);

    }
}