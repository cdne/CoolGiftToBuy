﻿using System.Collections.Generic;
using API.Entities;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/products")]
    
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository repository, IMapper mapper, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public IActionResult GetProducts([FromQuery] string name, [FromQuery] string description, 
            [FromQuery] string sortName)
        {
            var productsFromRepo = _repository.GetProducts(name,description,sortName);
            _logger.LogInformation("Show all products on a HTTP GET request.");
            return Ok(_mapper.Map<ICollection<ProductDto>>(productsFromRepo));
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            var product = _repository.GetProductById(id);
            if (product is null) return NotFound();
            _logger.LogInformation("Display a product on HTTP GET request.");
            return Ok(_mapper.Map<ProductDto>(product));
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        [Route("{id}/tags")]
        public IActionResult GetAllProductTags(int id)
        {
            var allTagsForProduct = _repository.GetTagsByProductId(id);
            
            return Ok(_mapper.Map<ICollection<TagDto>>(allTagsForProduct));
        }
        
        
        [HttpPost]        
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public ActionResult<ProductDto> AddProduct([FromBody] ProductForCreationDto product)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var productEntity = _mapper.Map<Product>(product);
            _repository.AddProduct(productEntity);
            
            var productToReturn = _mapper.Map<ProductDto>(productEntity);
            return CreatedAtAction(nameof(GetProduct), new {id = productEntity.Id, 
                version = ApiVersion.Default.ToString()}, productToReturn);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.1")]
        public ActionResult<ProductDto> UpdateProduct(int id, [FromBody] ProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest();
            var productToUpdate = _mapper.Map<Product>(product);
            product.Id = id;
            
            _repository.UpdateProduct(id, productToUpdate);
            
            return AcceptedAtAction(nameof(GetProduct), new {id = product.Id, 
                    version = ApiVersion.Default.ToString()}, 
                product);
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.1")]
        public IActionResult DeleteProduct(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.DeleteProductFromDatabase(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        [MapToApiVersion("1.1")]
        public IActionResult PartiallyUpdateProduct(int id,
            [FromBody] JsonPatchDocument<ProductDto> productPatch)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var product = _repository.GetProductById(id);
            var productDto = _mapper.Map<ProductDto>(product);
            
            productPatch.ApplyTo(productDto);
            
            _mapper.Map(productDto, product);
            _repository.Update(product);

            return AcceptedAtAction(nameof(GetProduct), new {id = productDto.Id, 
                    version = ApiVersion.Default.ToString()}, 
                productDto);
        }
    }
}