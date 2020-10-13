using System.Collections.Generic;
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
    /// <summary>
    /// Products controller
    /// </summary>
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
        
        /// <summary>
        /// Get all products on HttpGet request
        /// </summary>
        /// <param name="name">Query used for filtering products name</param>
        /// <param name="description">Query used for filtering products description</param>
        /// <param name="sortName">Query used for sorting name</param>
        /// <returns>Collection of products</returns>
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

        /// <summary>
        /// Get product by product id on HttpGet request
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product DTO</returns>
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
        
        /// <summary>
        /// Get all products tag by id on HttpGet request
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Tags collection</returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        [Route("{id}/tags")]
        public IActionResult GetAllProductTags(int id)
        {
            var allTagsForProduct = _repository.GetTagsByProductId(id);
            
            return Ok(_mapper.Map<ICollection<TagDto>>(allTagsForProduct));
        }
        
        /// <summary>
        /// Add product on HttpPost request
        /// </summary>
        /// <param name="product">Product model used for creating</param>
        /// <returns>Created product</returns>
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

        /// <summary>
        /// Update product on HttpPut request
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="product">Product DTO</param>
        /// <returns>Updated product</returns>
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
        
        /// <summary>
        /// Delete product on HttpDelete request
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>201 response</returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.1")]
        public IActionResult DeleteProduct(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.DeleteProductFromDatabase(id);
            return Ok();
        }

        /// <summary>
        /// Update product on HttpPatch request
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="productPatch">JsonPatchDocument of type ProductDto</param>
        /// <returns>Updated product</returns>
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