﻿using System.Collections.Generic;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public ActionResult GetAllCategories()
        {
            var categories = _repository.GetCategories();
            return Ok(_mapper.Map<ICollection<CategoryDto>>(categories));
        }
        

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public ActionResult GetCategoryById(int id)
        {
            var category = _repository.GetCategoryById(id);
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpGet("{id}/products")]
        [MapToApiVersion("1.0")]
        public IActionResult GetAllProductFromCategory(int id)
        {
            var productsByCategory = _repository.GetProductsByCategoryId(id);
            return Ok(_mapper.Map<ICollection<ProductDto>>(productsByCategory));
        }
    }
}