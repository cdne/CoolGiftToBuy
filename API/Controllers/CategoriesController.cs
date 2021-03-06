﻿using System.Collections.Generic;
using API.Entities;
using API.Models;
using API.Services;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    /// <summary>
    /// Controller for categories
    /// </summary>
    [Route("api/v{version:apiVersion}/categories")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryRepository repository, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all categories on HttpGet request
        /// </summary>
        /// <returns>Collection of categories</returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public ActionResult GetAllCategories()
        {
            var categories = _repository.GetCategories();
            return Ok(_mapper.Map<ICollection<CategoryDto>>(categories));
        }
        

        /// <summary>
        /// Get category by id on HttpGet request
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Category by id</returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public ActionResult GetCategoryById(int id)
        {
            var category = _repository.GetCategoryById(id);
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        /// <summary>
        /// Get all products from a category by category id on HttpGet request
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Collection with products by category id</returns>
        [HttpGet("{id}/products")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public IActionResult GetAllProductFromCategory(int id)
        {
            var productsByCategory = _repository.GetProductsByCategoryId(id);
            return Ok(_mapper.Map<ICollection<ProductDto>>(productsByCategory));
        }

        /// <summary>
        /// Add category on HttpPost request
        /// </summary>
        /// <param name="category">Category mapped from body</param>
        /// <returns>Created category</returns>
        [HttpPost]
        [MapToApiVersion("1.1")]
        public IActionResult AddCategory([FromBody] CategoryForCreationDto category)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var categoryToAdd = _mapper.Map<Category>(category);
            _repository.AddCategory(categoryToAdd);
            
            var categoryToReturn = _mapper.Map<CategoryDto>(categoryToAdd);
            
            _logger.LogInformation($"{categoryToReturn.Id}");
            
            return CreatedAtAction(nameof(GetCategoryById), 
                new {id = categoryToReturn.Id, version = ApiVersion.Default.ToString()}, 
                categoryToReturn);
        }

        /// <summary>
        /// Update category from HttpPut request
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="categoryDto">Category DTO</param>
        /// <returns>Updated category</returns>
        [HttpPut("{id}")]
        [MapToApiVersion("1.1")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var category = _mapper.Map<Category>(categoryDto);
            _repository.UpdateCategory(id, category);

            var categoryToReturn = _mapper.Map<CategoryDto>(category);
            categoryToReturn.Id = id;
        
            return AcceptedAtAction(nameof(GetCategoryById), 
                new {id = categoryToReturn.Id, version = ApiVersion.Default.ToString()},
                categoryToReturn);
        }

        /// <summary>
        /// Update category on HttpPatch request
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="patchCategory">JsonPatchDocument object</param>
        /// <returns>Updated category</returns>
        [HttpPatch("{id}")]
        [MapToApiVersion("1.1")]
        public IActionResult PartiallyUpdateCategory(int id, [FromBody] JsonPatchDocument<CategoryDto> patchCategory)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var category = _repository.GetCategoryById(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            
            patchCategory.ApplyTo(categoryDto);

            _mapper.Map(categoryDto, category);
            _repository.PartiallyUpdateCategory(category);
            
            return AcceptedAtAction(nameof(GetCategoryById), new {id = categoryDto.Id, 
                    version = ApiVersion.Default.ToString()}, 
                categoryDto);
        }

        
        /// <summary>
        /// Remove category on HttpDelete request
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>"Category was deleted" string on successful request</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            _repository.DeleteCategory(id);
            return Ok("Category was deleted");
        }
    }
}